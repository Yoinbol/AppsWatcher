using AppsWatcher.Common.Models;

namespace AppsWatcher.Repositories.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplicationTracksRepository : IRepository<ApplicationTrack>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool Save(ApplicationTrack instance);
    }
}
