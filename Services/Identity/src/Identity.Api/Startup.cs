using System.Collections.Generic;
using System.Reflection;
using Autofac;
using EventBus;
using EventBus.RabbitMQ;
using Identity.Api.Database;
using Identity.Api.Models;
using Identity.Api.Services;
using Identity.Api.Settings;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

namespace Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    opt => opt.MigrationsAssembly(migrationAssembly)
            ));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            Dictionary<string, string> clientUrls = new();

            clientUrls.Add("ProductsApi", Configuration.GetValue<string>("ProductsApiClient"));
            clientUrls.Add("OrdersApi", Configuration.GetValue<string>("OrdersApiClient"));
            clientUrls.Add("BasketApi", Configuration.GetValue<string>("BasketApiClient"));

            if (Environment.IsProduction())
            {
                services.AddIdentityServer()
                    .AddProfileService<ProfileService>()
                    .AddAspNetIdentity<ApplicationUser>()
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = builder => builder.UseNpgsql(
                            connectionString,
                            opt => opt.MigrationsAssembly(migrationAssembly));
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = builder => builder.UseNpgsql(
                            connectionString,
                            opt => opt.MigrationsAssembly(migrationAssembly));
                    })
                    .AddJwtBearerClientAuthentication()
                    .AddDeveloperSigningCredential();
            }
            else
            {
                services.AddIdentityServer()
                    .AddProfileService<ProfileService>()
                    .AddAspNetIdentity<ApplicationUser>()
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryApiScopes(Config.ApiScopes)
                    .AddInMemoryClients(Config.GetClients(clientUrls))
                    .AddInMemoryIdentityResources(Config.IdentityResources)
                    .AddInMemoryPersistedGrants()
                    .AddJwtBearerClientAuthentication()
                    .AddDeveloperSigningCredential();
            }

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:44368/";
                    options.RequireHttpsMetadata = false;
                });

            services.AddTransient<IProfileService, ProfileService>();

            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                )
            );

            services.AddTransient<IAuthService<ApplicationUser>, AuthService>();

            var jwtSettings = Configuration
                .GetSection(nameof(JwtSettings))
                .Get<JwtSettings>();

            services.AddSingleton(jwtSettings);

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

            services.AddControllers();
            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity.Api v1"));
            }

            app.UseCors();

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
