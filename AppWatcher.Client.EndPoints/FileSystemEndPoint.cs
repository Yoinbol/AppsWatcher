using System;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;

namespace AppWatcher.Client.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public class FileSystemEndPoint : EndPoint, IEndPoint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public Response Save(Session session)
        {
            return new Response { Succed = true };
        }

        /// <summary>
        /// 
        /// </summary>
        public int Interval
        {
            get 
            { 
                return 10; 
            }
        }
    }
}
