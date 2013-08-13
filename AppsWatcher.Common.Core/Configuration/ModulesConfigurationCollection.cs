using System.Configuration;

namespace AppsWatcher.Common.Core.Configuration
{
    [ConfigurationCollection(typeof(ModuleConfig))]
    public class ModulesConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ModuleConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModuleConfig)element).Components;
        }

        protected override string ElementName
        {
            get
            {
                return "Module";
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
    }
}
