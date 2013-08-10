using System;
using AppsWatcher.Client.EndPoints.Configuration;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;

namespace AppsWatcher.Client.EndPoints
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
        EndPointConfig Config { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime LastSave { get; set; }
    }
}
