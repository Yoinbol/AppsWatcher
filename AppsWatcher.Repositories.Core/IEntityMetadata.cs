using System.Collections.Generic;
using System.Reflection;
using AppsWatcher.Common.Models;
using AppsWatcher.Repositories.Core;

namespace AppsWatcher.Repositories.Core
{
    public interface IEntityMetadata<TEntity> where TEntity : DomainModel
    {
        string RepositoryName { get; }

        IEnumerable<ExtendedPropertyInfo> KeyProperties { get; }

        IEnumerable<PropertyMetadata> BaseProperties { get; }

        PropertyInfo IdentityProperty { get; }

        PropertyMetadata StatusProperty { get; }

        object LogicalDeleteValue { get; }

        bool LogicalDelete { get; }

        bool IsIdentity { get; }
    }
}
