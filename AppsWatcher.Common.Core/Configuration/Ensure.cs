using System.Configuration;

namespace AppsWatcher.Common.Core.Configuration
{
    [ConfigurationCollection(typeof(Component))]
    public class Ensure : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Component();
        }

        protected override string ElementName
        {
            get
            {
                return "Component";
            }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Component)element).ContractName;
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
