using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;

namespace EventBus.RabbitMQ
{
    public class PersistentConnection : IPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<PersistentConnection> _logger;
        private readonly int _retryCount;

        private IConnection _connection;
        private bool _disposed;
        object sync_root = new();

        public PersistentConnection(IConnectionFactory connectionFactory, ILogger<PersistentConnection> logger, int retryCount)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            _retryCount = retryCount;
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
            _logger.LogInformation("RabbitMQ Client is trying to connect");

            lock (sync_root)
            {
                var policy = RetryPolicy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (e, time) =>
                        {
                            _logger.LogWarning(e, $"RabbitMQ Client could not connect after {time.TotalSeconds:n1}s ({e.Message})");
                        }
                    );

                policy.Execute(() =>
                {
                    _connection = _connectionFactory.CreateConnection();
                });

                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;

                    _logger.LogInformation($"RabbitMQ Client acquired a persistent connection to '{_connection.Endpoint.HostName}' and is subscribed to failure events");

                    return true;
                }
                else
                {
                    _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");

                    return false;
                }
            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (!_disposed)
            {
                _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

                TryConnect();
            }
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (!_disposed)
            {
                _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

                TryConnect();
            }
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            if (!_disposed)
            {
                _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

                TryConnect();
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException e)
            {
                _logger.LogCritical(e.ToString());
            }

        }
    }
}
