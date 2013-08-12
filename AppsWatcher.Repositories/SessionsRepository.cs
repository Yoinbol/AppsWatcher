using System.Data;
using AppsWatcher.Common.Models;
using AppsWatcher.Repositories.Contracts;

namespace AppsWatcher.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionsRepository : Repository<Session>, ISessionsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public SessionsRepository(IDbConnection connection)
            : base(connection)
        { 
        }
    }
}
