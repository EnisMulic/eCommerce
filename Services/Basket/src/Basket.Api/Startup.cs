using Autofac;
using Basket.Api.Authorization;
using Basket.Api.Filters;
using Basket.Api.Grpc;
using Basket.Api.IntegrationEvents;
using Basket.Api.Repository;
using Basket.Api.Settings;
using Common.Basket.Authorization;
using EventBus;
using EventBus.IntegrationEventLog.Services;
using EventBus.RabbitMQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;

namespace Basket.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                )
            );

            services.AddGrpc();

            var grpcSettings = Configuration
                .GetSection("Grpc")
                .Get<GrpcSettings>();

            services.AddSingleton(grpcSettings);

            services.AddTransient<IProductGrpcClient, ProductGrpcClient>();

            services.AddAutoMapper(typeof(Mappings.BasketProfile));
            services.AddTransient<IBasketRepository, BasketRepository>();

            var redisSettings = Configuration
                .GetSection("Redis")
                .Get<RedisSettings>();


            services.AddSingleton(redisSettings);

            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(redisSettings.ConnectionString, true);

                configuration.ResolveDns = true;

                return ConnectionMultiplexer.Connect(configuration);
            });


            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(sp =>
                (DbConnection conn) => new IntegrationEventLogService(conn)
            );

            var rabbitMQSettings = Configuration
                .GetSection(RabbitMQSettings.RabbitMQ)
                .Get<RabbitMQSettings>();

            services.AddSingleton<IPersistentConnection>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<PersistentConnection>>();

                var factory = new ConnectionFactory
                {
                    HostName = rabbitMQSettings.HostName,
                    Port = rabbitMQSettings.Port,
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(rabbitMQSettings.UserName))
                {
                    factory.UserName = rabbitMQSettings.UserName;
                }

                if (!string.IsNullOrEmpty(rabbitMQSettings.Password))
                {
                    factory.Password = rabbitMQSettings.Password;
                }

                var retryCount = rabbitMQSettings.RetryCount;

                return new PersistentConnection(factory, logger, retryCount);
            });


            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var persistentConnection = sp.GetRequiredService<IPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var subscriptionClientName = Configuration["SubscriptionClientName"];
                var lifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var subscriptionManager = sp.GetRequiredService<ISubscriptionsManager>();
                var retryCount = rabbitMQSettings.RetryCount;

                return new EventBusRabbitMQ(persistentConnection: persistentConnection,
                    logger: logger,
                    subsManager: subscriptionManager,
                    queueName: subscriptionClientName,
                    autofac: lifetimeScope,
                    retryCount: retryCount
                );
            });

            services.AddSingleton<ISubscriptionsManager, InMemorySubscriptionsManager>();

            services.AddTransient<ProductPriceChangedIntegrationEventHandler>();

            services.AddControllers();

            var identityUrl = Configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = identityUrl;
                    options.Audience = BasketApi.Resource.Name;
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyConstants.BasketApiPolicy, policy =>
                {
                    policy.RequireClaim("permission", BasketApi.Resource.Name);
                });
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.Api", Version = "v1" });
                options.MapType<Guid>(() => new OpenApiSchema { Type = "string", Format = null });
                options.DescribeAllParametersInCamelCase();
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{identityUrl}/connect/authorize"),
                            TokenUrl = new Uri($"{identityUrl}/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                { BasketApi.Resource.Name, BasketApi.Resource.DisplayName },
                            }
                        }
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.Api V1");

                    options.OAuthClientId(BasketSwaggerClient.Id);
                    options.OAuthClientSecret("secret");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<ProductPriceChangedIntegrationEvent, ProductPriceChangedIntegrationEventHandler>();
        }
    }
}
