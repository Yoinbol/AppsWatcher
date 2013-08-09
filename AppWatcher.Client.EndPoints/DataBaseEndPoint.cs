using System;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Responses;

namespace AppWatcher.Client.EndPoints
{
    /// <summary>
    /// 
    /// </summary>
    public class DataBaseEndPoint : EndPoint, IEndPoint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public Response Save(Session session)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public int Interval
        {
            get { throw new NotImplementedException(); }
        }
    }
}
