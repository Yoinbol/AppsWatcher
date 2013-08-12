using System;
using System.Data;

namespace AppsWatcher.Repositories
{
    public class DataConnection : IDisposable
    {
        #region Properties

        private IDbConnection _connection;

        protected IDbConnection Connection
        {
            get
            {
                if (_connection.State != ConnectionState.Open && _connection.State != ConnectionState.Connecting)
                    _connection.Open();

                return _connection;
            }
        }

        public DataConnection(IDbConnection connection)
        {
            _connection = connection;
        }

        #endregion

        public void Dispose()
        {
            try
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                    _connection.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
