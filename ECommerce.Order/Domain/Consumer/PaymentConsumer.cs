using System.Text.Json;
using DotNetCore.CAP;
using ECommerce.Order.Domain.Entities.Enums;
using ECommerce.Order.Domain.Services;
using ECommerce.Order.Dtos;

namespace ECommerce.Order.Domain.Consumer
{
    public interface IPaymentConsumer
    {
        Task ProccessMessage(PaymentResponseDto paymentResponseDto);
    }

    public class PaymentConsumer : IPaymentConsumer, ICapSubscribe
    {
        private readonly IOrderService _orderService;

        public PaymentConsumer(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [CapSubscribe("ecomerce.order.payment")]
        public async Task ProccessMessage(PaymentResponseDto paymentResponseDto)
        {
            switch (paymentResponseDto.PaymentStatus)
            {
                case PaymentStatus.Accepted:
                    await _orderService.ChangeStatus(paymentResponseDto.OrderId, OrderStatus.Acepted,
                        paymentResponseDto.GatewayName, paymentResponseDto.TranzactionId);
                    return;
                case PaymentStatus.Rejected:
                    await _orderService.ChangeStatus(paymentResponseDto.OrderId, OrderStatus.Rejected,
                        paymentResponseDto.GatewayName, paymentResponseDto.TranzactionId);
                    break;
            }
        }
    }


}
