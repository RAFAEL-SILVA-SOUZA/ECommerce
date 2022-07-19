using ECommerce.Payment.Models;

namespace ECommerce.Payment.Domain.Services
{
    public interface IPaymentService
    {
        Task ProccessPayment(PaymentRequest paymentEntity);
    }
}
