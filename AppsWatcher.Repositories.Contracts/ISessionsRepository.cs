using System;
using System.Collections.Generic;
using AppsWatcher.Common.Models;

namespace AppsWatcher.Repositories.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISessionsRepository : IRepository<Session>
    {
        IEnumerable<Session> GetSessions(int page, int pageSize, DateTime? day = null, string userName = null);
    }
}
