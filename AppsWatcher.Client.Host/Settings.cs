using System.Configuration;

namespace AppsWatcher.Client.Host
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Settings
    {
        const int defaultInterval = 1000;

        /// <summary>
        /// 
        /// </summary>
        internal static int Interval
        {
            get
            {
                int interval;
                return int.TryParse(ConfigurationManager.AppSettings["Interval"], out interval) ? interval : defaultInterval;
            }
        }
    }
}
