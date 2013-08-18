using System;
using System.Linq;
using System.Reflection;
using AppsWatcher.Client.EndPoints.Configuration;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;
using log4net;

namespace AppsWatcher.Client.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class EndPoint : IEndPoint
    {
        /// <summary>
        /// 
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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

        /// <summary>
        /// 
        /// </summary>
        public string StorePath
        {
            get 
            {
                return Config.Settings.OfType<EndPointSetting>().FirstOrDefault(s => s.Name.Equals("StorePath", StringComparison.InvariantCultureIgnoreCase)).Value; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public virtual string GetStorePath(Session session)
        {
            return this.StorePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public abstract Response Save(Session session);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public abstract SingleResponse<Session> LoadSession(DateTime day, string userLogin);
    }
}
