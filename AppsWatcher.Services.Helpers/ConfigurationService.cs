using System;
using System.Configuration;
using AppsWatcher.Services.Helpers.Contracts;

namespace AppsWatcher.Services.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        /// <summary>
        /// 
        /// </summary>
        public bool AutoSaveUsers
        {
            get 
            { 
                return bool.Parse(ConfigurationManager.AppSettings["AutoSaveUsers"]); 
            }
        }
    }
}
