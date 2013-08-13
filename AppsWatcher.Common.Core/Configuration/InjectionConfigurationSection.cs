using System.Configuration;

namespace AppsWatcher.Common.Core.Configuration
{
    public class InjectionConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("Modules")]
        public ModulesConfigurationCollection Modules
        {
            get { return ((ModulesConfigurationCollection)(base["Modules"])); }
            set { base["Modules"] = value; }
        }

        [ConfigurationProperty("Components")]
        public ComponentsConfigurationCollection Components
        {
            get { return ((ComponentsConfigurationCollection)(base["Components"])); }
            set { base["Components"] = value; }
        }
    }
}
