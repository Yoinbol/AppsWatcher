using System.Reflection;
using AppsWatcher.Common.Models.Annotations;

namespace AppsWatcher.Repositories.Core
{
    public class PropertyMetadata
    {
        public PropertyMetadata(PropertyInfo propertyInfo)
        {
            this.Name = propertyInfo.Name;

            var alias = propertyInfo.GetCustomAttribute<Alias>();
            this.Alias = alias != null ? alias.Value : string.Empty;
        }

        public string Name { get; set; }

        public string Alias { get; set; }
    }
}
