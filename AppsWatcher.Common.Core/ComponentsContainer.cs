using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #endregion

        #region Functions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IContainer GetComponentsContainer()
        {
            lock (_lock)
            {
                return _componentsContainer ?? (_componentsContainer = this.Build());
            }
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
            this.RegisterInstance<TComponent>(component).As(component.GetType());
        }

        #endregion
    }
}
