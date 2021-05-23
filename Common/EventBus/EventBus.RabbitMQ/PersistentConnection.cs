using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.IO;

namespace EventBus.RabbitMQ
{
    public class PersistentConnection : IPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<PersistentConnection> _logger;
        
        private IConnection _connection;
        private bool _disposed;

        public PersistentConnection(IConnectionFactory connectionFactory, ILogger<PersistentConnection> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }

        public bool TryConnect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (IsConnected)
            {
                _logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", _connection.Endpoint.HostName);

                return true;
            }
            else
            {
                _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");

                return false;
            }
        }

        public void Dispose()
        {
            if(_disposed)
            {
                return;
            }

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch(IOException e)
            {
                _logger.LogCritical(e.ToString());
            }

        }
    }
}
