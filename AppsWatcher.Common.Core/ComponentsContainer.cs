using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using AppsWatcher.Common.Core.Configuration;
using Autofac;

namespace AppsWatcher.Common.Core
{
    public class ComponentsContainer : ContainerBuilder
    {
        private readonly static ComponentsContainer _instance = new ComponentsContainer();
        private IContainer _componentsContainer;
        private static object _lock;

        private ComponentsContainer() 
        {
            _lock = new object();

            LoadModulesConfiguration();
            LoadComponentsConfiguration();

            //Database connection
            if (!string.IsNullOrEmpty(Settings.ConnectionString))
            {
                this.Register(c => new SqlConnection(Settings.ConnectionString)).As<IDbConnection>().InstancePerLifetimeScope();
            }
        }

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public static ComponentsContainer Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private InjectionConfigurationSection Configuration
        {
            get
            {
                return (InjectionConfigurationSection)ConfigurationManager.GetSection("InjectionConfiguration");
            }
        }

        /// <summary>
        /// Modules to process
        /// </summary>
        private IEnumerable<ModuleConfig> Modules
        {
            get
            {
                return Configuration.Modules.OfType<ModuleConfig>();
            }
        }

        /// <summary>
        /// Components
        /// </summary>
        private IEnumerable<Component> Components
        {
            get
            {
                return Configuration.Components.OfType<Component>();
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IContainer GetComponentsContainer()
        {
            if (_componentsContainer == null)
            {
                lock (_lock)
                {
                    _componentsContainer = this.Build();
                }
            }

            return _componentsContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return GetComponentsContainer().Resolve<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ILifetimeScope BeginLifetimeScope()
        {
            return GetComponentsContainer().BeginLifetimeScope();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RegisterComponent<TComponent>(TComponent component) where TComponent : class
        {
            this.RegisterInstance<TComponent>(component).As<TComponent>();
        }

        #endregion

        #region Private utility

        /// <summary>
        /// 
        /// </summary>
        public void LoadModulesConfiguration()
        {
            foreach (var moduleConfig in Modules)
            {
                if (moduleConfig.AutoRegister)
                    ProcessAutoRegister(moduleConfig);

                if (moduleConfig.ForEachComponent.Count > 0)
                {
                    //Load the components assembly
                    var componentsAssembly = Assembly.Load(new AssemblyName(moduleConfig.Components));

                    IEnumerable<Type> moduleComponents = componentsAssembly.GetTypes()
                        .Where(c => !c.IsAbstract && !c.IsInterface && !c.IsGenericTypeDefinition && c.Namespace.Equals(moduleConfig.Components, StringComparison.InvariantCultureIgnoreCase));

                    if (moduleComponents.Any())
                    {
                        foreach (var config in moduleConfig.ForEachComponent)
                        {
                            RegisterGenericDependency(config as GenericOf, moduleComponents);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadComponentsConfiguration()
        {
            foreach (var componentConfig in Components)
            {
                ComponentDefinition componentDefinition = componentConfig.Type.ResolveComponentDefinition();
                Assembly componentAssembly = Assembly.Load(new AssemblyName(componentDefinition.AssemblyName));
                Type component = componentAssembly.GetType(componentDefinition.ComponentName);
                var register = this.RegisterType(component);

                if (!string.IsNullOrEmpty(componentConfig.Contract))
                {
                    ComponentDefinition contractDefinition = componentConfig.Contract.ResolveComponentDefinition();
                    Assembly contractAssembly = Assembly.Load(new AssemblyName(contractDefinition.AssemblyName));
                    Type contract = contractAssembly.GetType(string.Format(contractDefinition.ComponentName));
                    register = register.As(contract);
                }

                if (componentConfig.Singleton)
                    register.SingleInstance();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        private void ProcessAutoRegister(ModuleConfig module)
        {
            //Load the contracts assembly
            var contractsAssembly = Assembly.Load(new AssemblyName(module.Contracts));

            //Load the contracts
            var contracts = contractsAssembly.GetTypes()
                .Where(t => t.IsInterface && t.IsInNamespace(module.Contracts));

            if (contracts.Any())
            {
                //Load the components assembly
                var componentsAssembly = Assembly.Load(new AssemblyName(module.Components));

                foreach (var contract in contracts)
                {
                    Type component = module.TryGetForcedComponent(contract.Name);

                    if (component == null)
                    {
                        //No forced component, load the "regular"
                        //Sendspirations.DataServices.Contracts.IUserService
                        //Sendspirations.DataServices.UserService
                        component = componentsAssembly.GetType(ResolveComponentBaseName(componentsAssembly, contract), false);
                    }

                    //Ask if we found the component to register as the current contract
                    if (component != null && !component.IsAbstract)
                    {
                        var registry = this.RegisterType(component).As(contract);

                        //Ask if components for this module have to be "singletons"
                        if (module.Singletons)
                            registry.SingleInstance();

                        if (module.InstancePerLifetimeScope)
                            registry.InstancePerLifetimeScope();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genericOfConfig"></param>
        /// <param name="components"></param>
        private void RegisterGenericDependency(GenericOf genericOfConfig, IEnumerable<Type> components)
        {
            ComponentDefinition componentDefinition = genericOfConfig.Type.ResolveComponentDefinition();
            Assembly assembly = Assembly.Load(new AssemblyName(componentDefinition.AssemblyName));
            Type componentBase = assembly.GetType(string.Format("{0}`1", componentDefinition.ComponentName));

            foreach (var subType in components)
            {
                try
                {
                    Type componentType = componentBase.MakeGenericType(subType);
                    var registry = this.RegisterType(componentType);

                    if (!string.IsNullOrEmpty(genericOfConfig.Contract))
                    {
                        ComponentDefinition contractDefinition = genericOfConfig.Contract.ResolveComponentDefinition();
                        if (!assembly.GetName().Name.Equals(contractDefinition.AssemblyName, StringComparison.InvariantCultureIgnoreCase))
                            assembly = Assembly.Load(new AssemblyName(contractDefinition.AssemblyName));

                        Type contractTypeBase = assembly.GetType(string.Format("{0}`1", contractDefinition.ComponentName));
                        Type contractType = contractTypeBase.MakeGenericType(subType);
                        registry = registry.As(contractType);
                    }

                    //Ask if components for this module have to be "singletons"
                    if (genericOfConfig.Singletons)
                        registry = registry.SingleInstance();
                }
                catch (Exception ex)
                {
                    //TODO: improve they way how components could be filtered in order to remove this try/catch
                    string exx = ex.Message;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="contractType"></param>
        /// <returns></returns>
        private string ResolveComponentBaseName(Assembly assembly, Type contractType)
        {
            var del = string.Format("{0}.{1}", assembly.GetName().Name, this.GetComponentName(contractType));
            return string.Format("{0}.{1}", assembly.GetName().Name, this.GetComponentName(contractType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        private string GetComponentName(Type contractType)
        {
            string componentName = contractType.Name;

            if (contractType.IsInterface && contractType.Name.StartsWith("I"))
            {
                //Remove the initial "I" from the interface name
                componentName = componentName.Remove(0, 1);
            }

            return componentName;
        }

        #endregion
    }
}
