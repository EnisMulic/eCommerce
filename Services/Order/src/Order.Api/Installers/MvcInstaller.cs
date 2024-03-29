﻿using Common.Order.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Order.Api.Authorization;
using Order.Core.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace Order.Api.Installers
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

            services.AddControllers();

            services.AddScoped(typeof(IResponseBuilder<>), typeof(ResponseBuilder<>));

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = identityUrl;
                    options.Audience = OrderApi.Resource.Name;
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyConstants.OrderApiReadPolicy, policy =>
                {
                    policy.RequireClaim("permission", OrderApi.Scope.Read.Name);
                });

                options.AddPolicy(PolicyConstants.OrderApiWritePolicy, policy =>
                {
                    policy.RequireClaim("permission", OrderApi.Scope.Write.Name);
                });
            });
        }
    }
}
