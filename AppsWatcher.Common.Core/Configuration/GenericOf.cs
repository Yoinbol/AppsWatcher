using System.Configuration;

namespace AppsWatcher.Common.Core.Configuration
{
    public class GenericOf : Component
    {
        [ConfigurationProperty("singletons", IsRequired = false)]
        public bool Singletons
        {
            get
            {
                return (bool)this["singletons"];
            }
            set
            {
                this["singletons"] = value;
            }
        }
    }
}
