using System.Configuration;

namespace AppsWatcher.Client.EndPoints.Configuration
{
    public class EndPointsConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("EndPoints")]
        public EndPointsConfigurationCollection EndPoints
        {
            get { return ((EndPointsConfigurationCollection)(base["EndPoints"])); }
            set { base["EndPoints"] = value; }
        }
    }
}
