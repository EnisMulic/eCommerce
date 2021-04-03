using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Product.Api.Installers
{
    public class AutoMapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Mappings.ProductAttributeGroupProfile));
            services.AddAutoMapper(typeof(Mappings.ProductAttributeProfile));
            services.AddAutoMapper(typeof(Mappings.CategoryProfile));
            services.AddAutoMapper(typeof(Mappings.ProductProfile));
            services.AddAutoMapper(typeof(Mappings.ImageProfile));
        }
    }
}
