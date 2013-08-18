using System;
using System.Data;
using System.Reflection;
using log4net;

namespace AppsWatcher.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class DataConnection : IDisposable
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private IDbConnection _connection;

        /// <summary>
        /// 
        /// </summary>
        protected IDbConnection Connection
        {
            get
            {
                if (_connection.State != ConnectionState.Open && _connection.State != ConnectionState.Connecting)
                    _connection.Open();

                return _connection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public DataConnection(IDbConnection connection)
        {
            _connection = connection;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                    _connection.Close();
            }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                log.Error(ex.Message);
            }
        }
    }
}
