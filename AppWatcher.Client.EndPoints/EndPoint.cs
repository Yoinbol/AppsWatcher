using System;

namespace AppWatcher.Client.EndPoints
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
    }
}
