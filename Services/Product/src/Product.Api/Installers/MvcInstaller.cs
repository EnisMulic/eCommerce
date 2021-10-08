using Common.Product.Authorization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Product.Api.Authorization;
using Product.Api.Filters;
using Product.Core.Helpers;

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

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = identityUrl;
                    options.Audience = ProductApi.Resource.Name;
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyConstants.ProductApiReadPolicy, policy =>
                {
                    policy.RequireClaim("permission", ProductApi.Scope.Read.Name);
                });

                options.AddPolicy(PolicyConstants.ProductApiWritePolicy, policy =>
                {
                    policy.RequireClaim("permission", ProductApi.Scope.Write.Name);
                });

                options.AddPolicy(PolicyConstants.ProductApiDeletePolicy, policy =>
                {
                    policy.RequireClaim("permission", ProductApi.Scope.Delete.Name);
                });
            });
        }
    }
}
