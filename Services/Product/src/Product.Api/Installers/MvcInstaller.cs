using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Product.Api.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                )
            );

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMvc()
                .AddFluentValidation(configuration => 
                    configuration.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddControllers();
        }
    }
}
