using ECommerce.Order.Domain.Entities.Enums;
using ECommerce.Order.Models.Request;
using ECommerce.Order.Models.Response;

namespace ECommerce.Order.Domain.Services.Contracts
{
    public interface IPurchaseOrderService
    {
        Task<IList<PurchaseOrderResponse>> GetAllOrders();
        Task<PurchaseOrderResponse> GetOrderById(Guid id);
        Task<PurchaseOrderResponse> CreateOrder(PurchaseOrderCreateRequest purchaseOrderCreateRequest);
        Task<PurchaseOrderResponse> ReproccessOrder(PurchaseOrderReproccessRequest purchaseOrderReproccessRequest);
        Task ChangeStatus(Guid id, OrderStatus orderStatus, string gatewayName, Guid tranzactionId);
    }
}
