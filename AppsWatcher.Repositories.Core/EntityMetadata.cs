using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AppsWatcher.Common.Models;
using AppsWatcher.Common.Models.Annotations;

namespace AppsWatcher.Repositories.Core
{
    public class EntityMetadata<TEntity> : IEntityMetadata<TEntity> where TEntity : DomainModel
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string RepositoryName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ExtendedPropertyInfo> KeyProperties { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<PropertyMetadata> BaseProperties { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo IdentityProperty { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public PropertyMetadata StatusProperty { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public object LogicalDeleteValue { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool LogicalDelete
        {
            get
            {
                return this.StatusProperty != null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsIdentity
        {
            get
            {
                return this.IdentityProperty != null;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public EntityMetadata()
        {
            var entityType = typeof(TEntity);

            var aliasAttribute = entityType.GetCustomAttribute<Alias>();
            this.RepositoryName = aliasAttribute != null ? aliasAttribute.Value : entityType.Name;

            //Load all the "primitive" entity properties (and byte[])
            IEnumerable<PropertyInfo> props = entityType.GetProperties().Where(p => p.PropertyType.IsValueType || p.PropertyType.Name.Equals("String", StringComparison.InvariantCultureIgnoreCase) || p.PropertyType.Name.Equals("byte[]", StringComparison.InvariantCultureIgnoreCase));

            //Filter the base properties
            this.BaseProperties = props.Where(p => !p.GetCustomAttributes<CustomProperty>().Any() && !p.GetCustomAttributes<NonStored>().Any()).Select(p => new PropertyMetadata(p));

            //Filter key properties
            this.KeyProperties = props.Where(p => p.GetCustomAttributes<KeyProperty>().Any()).Select(p => new ExtendedPropertyInfo(p));

            //Identity??
            this.IdentityProperty = props.SingleOrDefault(p => p.GetCustomAttributes<KeyProperty>().Any(k => k.Identity));

            //Status property (if exists, and if it does, it must be an enumeration)
            var statusProperty = props.FirstOrDefault(p => p.PropertyType.IsEnum && p.GetCustomAttributes<StatusProperty>().Any());

            if (statusProperty != null)
            {
                this.StatusProperty = new PropertyMetadata(statusProperty);
                var statusPropertyType = statusProperty.PropertyType;
                var deleteOption = statusPropertyType.GetFields().FirstOrDefault(f => f.GetCustomAttribute<Deleted>() != null);

                if (deleteOption != null)
                {
                    var enumValue = Enum.Parse(statusPropertyType, deleteOption.Name);

                    if (enumValue != null)
                        this.LogicalDeleteValue = Convert.ChangeType(enumValue, Enum.GetUnderlyingType(statusPropertyType));
                }
            }
        }
    }
}
