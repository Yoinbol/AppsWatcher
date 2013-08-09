using System.Collections.Generic;
using System.Linq;

namespace AppsWatcher.Repositories.Core
{
    public static class Extensions
    {
        public static IEnumerable<string> GetPropertyNames(this object instance)
        {
            return instance.GetType().GetProperties().Select(p => new ExtendedPropertyInfo(p).ColumnName);
        }
    }
}
