using ECommerce.Order.Domain.Entities.Enums;
using ECommerce.Order.Dtos;

namespace ECommerce.Order.Domain.Services
{
    public interface IOrderService
    {
        Task<IList<OrderEnityDto>> GetAllOrders();
        Task<OrderEnityDto?> GetOrderById(Guid id);
        Task CreateOrder(OrderCreateDto orderCreateDto);
        Task ChangeStatus(Guid id, OrderStatus orderStatus);
    }
}
