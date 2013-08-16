using System.Data;
using AppsWatcher.Common.Models;
using AppsWatcher.Repositories.Contracts;

namespace AppsWatcher.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public UsersRepository(IDbConnection connection)
            : base(connection)
        { 
        }
    }
}
