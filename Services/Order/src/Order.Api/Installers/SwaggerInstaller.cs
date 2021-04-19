using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Order.Api.Filters;
using System;
using System.Collections.Generic;

namespace Order.Api.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                var identityUrl = configuration.GetValue<string>("IdentityUrl");

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.Api", Version = "v1" });
                options.MapType<Guid>(() => new OpenApiSchema { Type = "string", Format = null });
                options.DescribeAllParametersInCamelCase();
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        ClientCredentials = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{identityUrl}/connect/authorize"),
                            TokenUrl = new Uri($"{identityUrl}/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                { "order-api", "Order Api" }
                            }
                        }
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }
    }
}
