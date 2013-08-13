using System.Configuration;

namespace AppsWatcher.Common.Core.Configuration
{
    [ConfigurationCollection(typeof(Component))]
    public class ComponentsConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Component();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Component)element).Type;
        }

        protected override string ElementName
        {
            get
            {
                return "Component";
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
