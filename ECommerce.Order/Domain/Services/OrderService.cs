using ECommerce.Order.Domain.Entities;
using ECommerce.Order.Domain.Entities.Enums;
using ECommerce.Order.Dtos;
using ECommerce.Order.Infra;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Order.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly IProductService _productService;

        public OrderService(OrderDbContext orderDbContext, IProductService productService)
        {
            _orderDbContext = orderDbContext;
            _productService = productService;
        }

        public async Task<IList<OrderEnityDto>> GetAllOrders()
        {
            var orders = await _orderDbContext.Orders
                .Include(X=>X.Itens)
                .AsNoTracking()
                .ToListAsync();
            return orders.Select(x => new OrderEnityDto(x)).ToList();
        }

        public async Task<OrderEnityDto?> GetOrderById(Guid id)
        {
            var order = await _orderDbContext.Orders
                .Include(X=>X.Itens)
                .FirstOrDefaultAsync(X=>X.Id == id);
            return order == null ? default : new OrderEnityDto(order);
        }

        public async Task CreateOrder(OrderCreateDto orderCreateDto)
        {
            var order = OrderEntity.Instance;
            var items = await _productService
                .GetProducts(orderCreateDto.Itens.Select(x => x.ItemId)
                    .ToArray());

            foreach (var item in items)
                order.AddItens(new OrderItem(item.Description, item.Price));

            await _orderDbContext.Orders.AddAsync(order);
            await _orderDbContext.SaveChangesAsync();
        }

        public async Task ChangeStatus(Guid id, OrderStatus orderStatus)
        {
            var order = await _orderDbContext.Orders.FindAsync(id);
            if (order != null) order.OrderStatus = orderStatus;
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
