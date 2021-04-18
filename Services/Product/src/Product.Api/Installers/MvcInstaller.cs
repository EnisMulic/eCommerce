using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Api.Filters;
using Product.Core.Helpers;
using System.IdentityModel.Tokens.Jwt;

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

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvc(options => 
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(configuration => 
                configuration.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddControllers();

            services.AddScoped(typeof(IResponseBuilder<>), typeof(ResponseBuilder<>));


            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //}).AddJwtBearer(options =>
            //{
            //    options.Authority = identityUrl;
            //    options.RequireHttpsMetadata = false;
            //    options.Audience = "products";
            //});

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    // required audience of access tokens
                    options.ApiName = "products";

                    // auth server base endpoint (this will be used to search for disco doc)
                    options.Authority = identityUrl;
                });
        }
    }
}
