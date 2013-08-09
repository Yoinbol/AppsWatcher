using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AppsWatcher.Common.Core;
using AppsWatcher.Common.Models;

namespace AppsWatcher.Repositories.Core
{
    public class QueryGenerator<TEntity> : IQueryGenerator<TEntity> where TEntity : DomainModel
    {
        #region Constructors

        public QueryGenerator()
        {
            this.EntityMetadata = ComponentsContainer.Instance.Resolve<IEntityMetadata<TEntity>>();
        }

        #endregion

        #region Properties

        protected IEntityMetadata<TEntity> EntityMetadata { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsIdentity
        {
            get
            {
                return this.EntityMetadata.IsIdentity;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo IdentityProperty
        {
            get
            {
                return this.EntityMetadata.IdentityProperty;
            }
        }

        #endregion

        #region Query generators

        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public string GetAddQuery()
        {
            //Enumerate the entity properties
            //Identity property (if exists) has to be ignored
            IEnumerable<PropertyMetadata> properties = (this.EntityMetadata.IsIdentity ?
                                                        this.EntityMetadata.BaseProperties.Where(p => !p.Name.Equals(this.EntityMetadata.IdentityProperty.Name, StringComparison.InvariantCultureIgnoreCase)) :
                                                        this.EntityMetadata.BaseProperties).ToList();

            string columNames = this.GetMetadata(properties, string.Empty, false);
            string values = this.GetMetadata(properties, "@", false);

            string query = string.Format("INSERT INTO {0} {1} {2}",
                                         this.EntityMetadata.RepositoryName,
                                         string.IsNullOrEmpty(columNames) ? string.Empty : string.Format("({0})", columNames),
                                         string.IsNullOrEmpty(values) ? string.Empty : string.Format(" VALUES ({0})", values));

            //If the entity has an identity key, we create a new variable into the query in order to streo the generated id
            if (this.IsIdentity)
            {
                query += Environment.NewLine + "DECLARE @NEWID NUMERIC(38, 0)";
                query += Environment.NewLine + "SET	@NEWID = SCOPE_IDENTITY()";
                query += Environment.NewLine + "SELECT @NEWID";
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetUpdateQuery()
        {
            var properties = this.EntityMetadata.BaseProperties.Where(p => !this.EntityMetadata.KeyProperties.Any(k => k.PropertyMetadata.Name.Equals(p.Name, StringComparison.InvariantCultureIgnoreCase)));

            string query = string.Format("UPDATE {0} SET {1} WHERE {2}",
                                         this.EntityMetadata.RepositoryName,
                                         this.ToSet(properties),
                                         this.ToWhere(this.EntityMetadata.KeyProperties.Select(p => p.ColumnName), this.EntityMetadata.RepositoryName));

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetByKeyQuery()
        {
            var keyNames = this.EntityMetadata.KeyProperties.Select(p => p.ColumnName);

            return this.GetSelectQuery(keyNames);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSelectAllQuery()
        {
            return this.GetSelectQuery(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public string GetSelectQuery(IEnumerable<string> filters)
        {
            //This repository does not have any custom property
            var query = string.Format("SELECT {0} FROM {1} WITH (NOLOCK)",
                                    this.GetMetadata(this.EntityMetadata.BaseProperties),
                                    this.EntityMetadata.RepositoryName);

            bool containsFilter = (filters != null && filters.Any());

            if (containsFilter)
                query += " WHERE " + this.ToWhere(filters, this.EntityMetadata.RepositoryName);

            //Evaluates if this repository implements logical delete
            if (this.EntityMetadata.LogicalDelete)
            {
                if (containsFilter)
                    query += string.Format(" AND {0}.{1} != {2}", 
                                            this.EntityMetadata.RepositoryName, 
                                            this.EntityMetadata.StatusProperty.Name, 
                                            this.EntityMetadata.LogicalDeleteValue);
                else
                    query += string.Format(" WHERE {0}.{1} != {2}", 
                                            this.EntityMetadata.RepositoryName, 
                                            this.EntityMetadata.StatusProperty.Name, 
                                            this.EntityMetadata.LogicalDeleteValue);
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetDeleteQuery()
        {
            string query;

            if (!this.EntityMetadata.LogicalDelete)
            {
                query = string.Format("DELETE FROM {0} WHERE {1}",
                                      this.EntityMetadata.RepositoryName,
                                      this.ToWhere(this.EntityMetadata.KeyProperties.Select(p => p.ColumnName), this.EntityMetadata.RepositoryName));
            }
            else
                query = this.GetLogicalDeleteQuery();

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetLogicalDeleteQuery()
        {
            return string.Format("UPDATE {0} SET {1} WHERE {2}",
                                    this.EntityMetadata.RepositoryName,
                                    string.Format("{0} = {1}", this.EntityMetadata.StatusProperty.Name, this.EntityMetadata.LogicalDeleteValue),
                                    this.ToWhere(this.EntityMetadata.KeyProperties.Select(p => p.ColumnName), this.EntityMetadata.RepositoryName));
        }

        #endregion

        #region Private utility

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        protected string GetMetadata(IEnumerable<PropertyMetadata> properties, string prefix, bool includeAlias = true)
        {
            var baseProp = "[{0}]";
            if (!string.IsNullOrEmpty(prefix))
                baseProp = prefix + "{0}";

            return string.Join(", ", properties.Select(p =>
                {
                    if (includeAlias && !string.IsNullOrEmpty(p.Alias))
                        return string.Format(baseProp, p.Alias) + " AS " + p.Name;
                    
                    return string.Format(baseProp, p.Name);
                }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected string GetMetadata(IEnumerable<PropertyMetadata> properties)
        {
            return this.GetMetadata(properties, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string ToWhere(IEnumerable<string> properties, string tableName)
        {
            return string.Join(" AND ", properties.Select(p => string.Format("{0}.{1} = @{1}", tableName, p)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected string ToSet(IEnumerable<PropertyMetadata> properties)
        {
            return string.Join(", ", properties.Select(p => string.Format("{0} = @{1}", !string.IsNullOrEmpty(p.Alias) ? p.Alias : p.Name, p.Name)));
        }

        #endregion
    }
}
