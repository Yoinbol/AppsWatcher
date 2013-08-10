using System.Configuration;

namespace AppsWatcher.Client.EndPoints.Configuration
{
    public class EndPointConfig : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
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

        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly
        {
            get
            {
                return (string)this["assembly"];
            }
            set
            {
                this["assembly"] = value;
            }
        }

        [ConfigurationProperty("interval", IsRequired = true)]
        public int Interval
        {
            get
            {
                return (int)this["interval"];
            }
            set
            {
                this["interval"] = value;
            }
        }

        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = true)]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
            set
            {
                this["enabled"] = value;
            }
        }

        [ConfigurationProperty("Settings", IsRequired = false)]
        public EndPointSettingsConfigurationCollection Settings
        {
            get
            {
                return (EndPointSettingsConfigurationCollection)this["Settings"];
            }
            set
            {
                this["Settings"] = value;
            }
        }
    }
}
