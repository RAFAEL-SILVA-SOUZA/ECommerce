using DotNetCore.CAP;
using ECommerce.Order.Domain.Entities.Enums;
using ECommerce.Order.Domain.Services.Contracts;
using ECommerce.Order.Models.Response;

namespace ECommerce.Order.Domain.Consumer
{
    public class OrderStatusOrderStatusPaymentConsumer : IOrderStatusPaymentConsumer, ICapSubscribe
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public OrderStatusOrderStatusPaymentConsumer(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        [CapSubscribe("ecomerce.order.status.payment")]
        public async Task ProccessMessage(PurchaseOrderPaymentResponse purchaseOrderPaymentResponse)
        {
            switch (purchaseOrderPaymentResponse.PaymentStatus)
            {
                case PaymentStatus.Accepted:
                    await _purchaseOrderService.ChangeStatus(purchaseOrderPaymentResponse.OrderId, OrderStatus.Acepted,
                        purchaseOrderPaymentResponse.GatewayName, purchaseOrderPaymentResponse.TranzactionId);
                    return;
                case PaymentStatus.Rejected:
                    await _purchaseOrderService.ChangeStatus(purchaseOrderPaymentResponse.OrderId, OrderStatus.Rejected,
                        purchaseOrderPaymentResponse.GatewayName, purchaseOrderPaymentResponse.TranzactionId);
                    break;
            }
        }
    }


}
