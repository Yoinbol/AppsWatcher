using System.Configuration;

namespace AppsWatcher.Common.Core.Configuration
{
    [ConfigurationCollection(typeof(GenericOf))]
    public class ForEachComponent : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new GenericOf();
        }

        protected override string ElementName
        {
            get
            {
                return "GenericOf";
            }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GenericOf)element).Type;
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
