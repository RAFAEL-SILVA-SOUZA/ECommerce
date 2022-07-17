using ECommerce.Order.Domain.Services;
using ECommerce.Order.Infra;

namespace ECommerce.Order.Extensions
{
    public static class ServicesResolver
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddDbContext<OrderDbContext>();
        }
    }
}
