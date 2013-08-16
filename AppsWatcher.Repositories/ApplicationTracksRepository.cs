using System.Data;
using AppsWatcher.Common.Models;
using AppsWatcher.Repositories.Contracts;
using Dapper;

namespace AppsWatcher.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationTracksRepository : Repository<ApplicationTrack>, IApplicationTracksRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public ApplicationTracksRepository(IDbConnection connection)
            : base(connection)
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool Save(ApplicationTrack instance)
        {
            return Connection.Execute("SaveApplicationTrack", instance, commandType: CommandType.StoredProcedure) > 0;
        }
    }
}
