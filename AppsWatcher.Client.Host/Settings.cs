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

        /// <summary>
        /// 
        /// </summary>
        internal static bool ShowIcon
        {
            get
            {
                var showIcon = false;
                return bool.TryParse(ConfigurationManager.AppSettings["ShowIcon"], out showIcon) ? showIcon : false;
            }
        }
    }
}
