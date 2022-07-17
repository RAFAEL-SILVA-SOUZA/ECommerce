using ECommerce.Payment.Domain.Entity;

namespace ECommerce.Payment.Domain.Services
{
    public interface IPaymentService
    {
        Task ProccessPayment(PaymentEntity paymentEntity);
    }
}
