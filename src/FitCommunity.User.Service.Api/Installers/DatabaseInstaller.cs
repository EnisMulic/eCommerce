using FitCommunity.User.Service.Core;
using FitCommunity.User.Service.Core.Interfaces;
using FitCommunity.User.Service.Database;
using FitCommunity.User.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitCommunity.User.Service.Api.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserDbContext, UserDbContext>();

            services.AddScoped<IUserDbContext, UserDbContext>();
            services.AddScoped<IRoleService, RoleService>();

        }
    }
}
