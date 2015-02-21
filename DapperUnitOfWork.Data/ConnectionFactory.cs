using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnitOfWork.Data
{
    internal class ConnectionFactory
    {
        private readonly DbProviderFactory _providerFactory;
        private readonly string _connectionString;
        private readonly string _providerName;
        private readonly string _connectionName;

        public ConnectionFactory(string connectionName)
        {
            if (string.IsNullOrWhiteSpace(connectionName))
                throw new ArgumentException("String argument may not be null, empty, or composed entirely of white space.", "connectionName");

            var connectionString = ConfigurationManager.ConnectionStrings[connectionName];
            if (connectionString == null)
                throw new ConfigurationErrorsException(string.Format("Failed to find connection string '{0}' in application configuration.", connectionName));

            var providerName = connectionString.ProviderName;
            if (string.IsNullOrWhiteSpace(providerName))
                throw new ConfigurationErrorsException(string.Format("No provider specified for connection string '{0}' in application configuration.", connectionName));

            var providerFactory = DbProviderFactories.GetFactory(providerName);
            if (providerFactory == null)
                throw new ConfigurationErrorsException(string.Format("Could not create provider factory for '{0}', for connection string '{1}' in application configuration.", providerName, connectionName));

            _connectionName = connectionName;
            _providerName = providerName;
            _providerFactory = providerFactory;
            _connectionString = connectionString.ConnectionString;
        }

        public IDbConnection Create()
        {
            var connection = _providerFactory.CreateConnection();

            if (connection == null)
                throw new ConfigurationErrorsException(string.Format("Could not create connection using connection string '{0}' in application configuration.", _connectionName));

            connection.ConnectionString = _connectionString;

            return connection;
        }
    }
}
