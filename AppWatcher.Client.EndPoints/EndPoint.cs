using System;
using AppsWatcher.Client.EndPoints.Configuration;

namespace AppsWatcher.Client.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class EndPoint
    {
        /// <summary>
        /// 
        /// </summary>
        protected DateTime _lastSave;

        /// <summary>
        /// 
        /// </summary>
        protected EndPointConfig _config;

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastSave
        {
            get
            {
                return _lastSave;
            }
            set
            {
                _lastSave = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public EndPointConfig Config
        {
            get
            {
                return _config;
            }
            set
            {
                _config = value;
            }
        }
    }
}
