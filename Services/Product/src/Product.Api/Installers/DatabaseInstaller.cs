using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Core.Interfaces;
using Product.Database;
using Product.Services;

namespace Product.Api.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IImageUploadService, ImageUploadService>();

            services.AddScoped<IProductAttributeGroupService, ProductAttributeGroupService>();

            services.AddScoped<IProductOptionsService, ProductOptionService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductService, ProductService>();
        }
    }
}
