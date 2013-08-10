using System.Configuration;

namespace AppsWatcher.Client.EndPoints.Configuration
{
    [ConfigurationCollection(typeof(EndPointSetting))]
    public class EndPointSettingsConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EndPointSetting();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EndPointSetting)element).Name;
        }

        protected override string ElementName
        {
            get
            {
                return "Setting";
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
