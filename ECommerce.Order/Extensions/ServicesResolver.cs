using ECommerce.Order.Domain.Consumer;
using ECommerce.Order.Domain.Services;
using ECommerce.Order.Infra;

namespace ECommerce.Order.Extensions
{
    public static class ServicesResolver
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IPaymentConsumer, PaymentConsumer>();
            services.AddTransient<ICatalogItemService, CatalogItemService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddDbContext<OrderDbContext>();
        }
    }
}
