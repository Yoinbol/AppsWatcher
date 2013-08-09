using System.Reflection;

namespace AppsWatcher.Repositories.Core
{
    public class ExtendedPropertyInfo
    {
        public PropertyInfo PropertyInfo { get; private set; }
        public PropertyMetadata PropertyMetadata { get; private set; }

        public ExtendedPropertyInfo(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
            this.PropertyMetadata = new PropertyMetadata(propertyInfo);
        }

        public string ColumnName
        {
            get 
            {
                return string.IsNullOrEmpty(this.PropertyMetadata.Alias) ? this.PropertyInfo.Name : this.PropertyMetadata.Alias;
            }
        }
    }
}
