using ECommerce.Catalog.Domain.Consumer;
using ECommerce.Catalog.Infra;

namespace ECommerce.Catalog.Extensions
{
    public static class ServicesResolver
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IProductConsumer, ProductConsumer>();
            services.AddDbContext<CatalogDBContext>();
        }
    }
}
