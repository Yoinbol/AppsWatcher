using System.Configuration;

namespace AppsWatcher.Common.Core.Configuration
{
    public class ModuleConfig : ConfigurationElement
    {
        [ConfigurationProperty("contracts", IsRequired = false)]
        public string Contracts
        {
            get
            {
                return (string)this["contracts"];
            }
            set
            {
                this["contracts"] = value;
            }
        }

        [ConfigurationProperty("components", IsRequired = true)]
        public string Components
        {
            get
            {
                return (string)this["components"];
            }
            set
            {
                this["components"] = value;
            }
        }

        [ConfigurationProperty("autoRegister", IsRequired = false, DefaultValue = true)]
        public bool AutoRegister
        {
            get
            {
                return (bool)this["autoRegister"];
            }
            set
            {
                this["autoRegister"] = value;
            }
        }

        [ConfigurationProperty("singletons", IsRequired = false)]
        public bool Singletons
        {
            get
            {
                return (bool)this["singletons"];
            }
            set
            {
                this["singletons"] = value;
            }
        }

        [ConfigurationProperty("instancePerLifetimeScope", IsRequired = false)]
        public bool InstancePerLifetimeScope
        {
            get
            {
                return (bool)this["instancePerLifetimeScope"];
            }
            set
            {
                this["instancePerLifetimeScope"] = value;
            }
        }

        [ConfigurationProperty("ForEachComponent", IsRequired = false)]
        public ForEachComponent ForEachComponent
        {
            get
            {
                return (ForEachComponent)this["ForEachComponent"];
            }
            set
            {
                this["ForEachComponent"] = value;
            }
        }

        [ConfigurationProperty("Ensure", IsRequired = false)]
        public Ensure Ensure
        {
            get
            {
                return (Ensure)this["Ensure"];
            }
            set
            {
                this["Ensure"] = value;
            }
        }
    }
}
