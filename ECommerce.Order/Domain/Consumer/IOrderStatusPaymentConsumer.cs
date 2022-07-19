using ECommerce.Order.Models.Response;

namespace ECommerce.Order.Domain.Consumer;

public interface IOrderStatusPaymentConsumer
{
    Task ProccessMessage(PurchaseOrderPaymentResponse purchaseOrderPaymentResponse);
}