using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AppsWatcher.Common.Core;
using AppsWatcher.Common.Models;
using AppsWatcher.Repositories.Contracts;
using AppsWatcher.Repositories.Core;
using Dapper;

namespace AppsWatcher.Repositories
{
    public abstract class Repository<TEntity> : DataConnection, IRepository<TEntity> where TEntity : DomainModel
    {

        #region Constructors

        protected Repository(IDbConnection connection)
            : base(connection)
        {
            this.QueryGenerator = ComponentsContainer.Instance.Resolve<IQueryGenerator<TEntity>>();
        }

        #endregion

        #region Properties

        private IQueryGenerator<TEntity> QueryGenerator;

        #endregion

        #region Repository base actions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual TEntity Parse(dynamic data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Connection.Query<TEntity>(this.QueryGenerator.GetSelectAllQuery());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetWhere(object filters)
        {
            return Connection.Query<TEntity>(this.QueryGenerator.GetSelectQuery(filters.GetPropertyNames()), filters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual TEntity GetFirst(object filters)
        {
            return this.GetWhere(filters).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        private TEntity GetByKey(TEntity instance)
        {
            return Connection.Query<TEntity>(this.QueryGenerator.GetByKeyQuery(), instance).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual bool Add(TEntity instance)
        {
            bool added = false;

            if (this.QueryGenerator.IsIdentity)
            {
                var newId = Connection.Query<decimal>(this.QueryGenerator.GetAddQuery(), instance).Single();
                added = newId > 0;

                if (added)
                    this.QueryGenerator.IdentityProperty.SetValue(instance, Convert.ChangeType(newId, this.QueryGenerator.IdentityProperty.PropertyType));
            }
            else
            {
                TEntity storedEntity = this.GetByKey(instance);

                if (storedEntity == null)
                    added = Connection.Execute(this.QueryGenerator.GetAddQuery(), instance) > 0;
            }

            return added;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool Delete(object key)
        {
            return Connection.Execute(this.QueryGenerator.GetDeleteQuery(), key) > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual bool Update(TEntity instance)
        {
            return Connection.Execute(this.QueryGenerator.GetUpdateQuery(), instance) > 0;
        }

        #endregion
    }
}
