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
    }
}
