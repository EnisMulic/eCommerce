using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System;

namespace Product.Api.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcCore().AddApiExplorer().AddNewtonsoftJson(options =>
                options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            // order is vital, this *must* be called *after* AddNewtonsoftJson()
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product.Api", Version = "v1" });
                c.MapType<Guid>(() => new OpenApiSchema { Type = "string", Format = null });
                c.DescribeAllParametersInCamelCase();
            });
        }
    }
}
