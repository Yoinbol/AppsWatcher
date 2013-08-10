using System.Configuration;

namespace AppsWatcher.Client.EndPoints.Configuration
{
    [ConfigurationCollection(typeof(EndPointConfig))]
    public class EndPointsConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EndPointConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EndPointConfig)element).Type;
        }

        protected override string ElementName
        {
            get
            {
                return "EndPoint";
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
