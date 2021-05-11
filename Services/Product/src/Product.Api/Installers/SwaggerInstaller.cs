using Common.Product.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Product.Api.Filters;
using System;
using System.Collections.Generic;

namespace Product.Api.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcCore().AddApiExplorer().AddNewtonsoftJson(options =>
                options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            // order is vital, this *must* be called *after* AddNewtonsoftJson()
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Product.Api", Version = "v1" });
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
                                { ProductApi.Scope.Read.Name, ProductApi.Scope.Read.DisplayName },
                                { ProductApi.Scope.Write.Name, ProductApi.Scope.Write.DisplayName },
                                { ProductApi.Scope.Delete.Name, ProductApi.Scope.Delete.DisplayName }
                            }
                        }
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }
    }
}
