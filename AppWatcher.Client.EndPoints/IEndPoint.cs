using System;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;

namespace AppWatcher.Client.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEndPoint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        Response Save(Session session);

        /// <summary>
        /// 
        /// </summary>
        int Interval { get; }

        /// <summary>
        /// 
        /// </summary>
        DateTime LastSave { get; set; }
    }
}
