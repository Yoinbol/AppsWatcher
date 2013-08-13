using System.Configuration;

namespace AppsWatcher.Common.Core.Configuration
{
    public class Component : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = false)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }

        [ConfigurationProperty("contract", IsRequired = false)]
        public string Contract
        {
            get
            {
                return (string)this["contract"];
            }
            set
            {
                this["contract"] = value;
            }
        }

        [ConfigurationProperty("contractName", IsRequired = false)]
        public string ContractName
        {
            get
            {
                return (string)this["contractName"];
            }
            set
            {
                this["contractName"] = value;
            }
        }

        [ConfigurationProperty("singleton", IsRequired = false, DefaultValue = false)]
        public bool Singleton
        {
            get
            {
                return (bool)this["singleton"];
            }
            set
            {
                this["singleton"] = value;
            }
        }
    }
}
