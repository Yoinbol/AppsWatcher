using System.Collections.Generic;
using System.Reflection;
using AppsWatcher.Common.Models;

namespace AppsWatcher.Repositories.Core
{
    public interface IQueryGenerator<TEntity>
        where TEntity : DomainModel
    {
        bool IsIdentity { get; }

        PropertyInfo IdentityProperty { get; }

        string GetSelectAllQuery();

        string GetByKeyQuery();

        string GetSelectQuery(IEnumerable<string> filters);

        string GetAddQuery();

        string GetUpdateQuery();

        string GetDeleteQuery();
    }
}
