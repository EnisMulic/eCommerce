using Autofac;
using EventBus;
using EventBus.IntegrationEventLog.Services;
using EventBus.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Product.Api.Options;
using Product.Core.Interfaces;
using Product.Services;
using RabbitMQ.Client;
using System;
using System.Data.Common;

namespace Product.Api.Installers
{
    public class RabbitMQInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(sp =>
                (DbConnection conn) => new IntegrationEventLogService(conn)
            );
            
            var settings = configuration.GetSection(RabbitMQOptions.RabbitMQ).Get<RabbitMQOptions>();

            services.AddSingleton<IPersistentConnection>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<PersistentConnection>>();

                var factory = new ConnectionFactory
                {
                    HostName = settings.HostName,
                    Port = settings.Port,
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(settings.UserName))
                {
                    factory.UserName = settings.UserName;
                }

                if (!string.IsNullOrEmpty(settings.Password))
                {
                    factory.Password = settings.Password;
                }

                var retryCount = settings.RetryCount;

                return new PersistentConnection(factory, logger, retryCount);
            });


            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var persistentConnection = sp.GetRequiredService<IPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var subscriptionClientName = configuration["SubscriptionClientName"];
                var lifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var subscriptionManager = sp.GetRequiredService<ISubscriptionsManager>();
                var retryCount = settings.RetryCount;

                return new EventBusRabbitMQ(persistentConnection: persistentConnection, 
                    logger: logger, 
                    subsManager: subscriptionManager, 
                    queueName: subscriptionClientName, 
                    autofac: lifetimeScope, 
                    retryCount: retryCount
                );
            });

            services.AddSingleton<ISubscriptionsManager, InMemorySubscriptionsManager>();

            services.AddTransient<IProductIntegrationEventService, ProductIntegrationEventService>();
        }
    }
}