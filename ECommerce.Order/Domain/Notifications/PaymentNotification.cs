using MediatR;

namespace ECommerce.Order.Domain.Notifications
{
    public class PaymentNotification :  INotification
    {
        public PaymentNotification(string cardName, string cardNumber, string validDate, string cvv, Guid orderId, decimal amount)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            ValidDate = validDate;
            Cvv = cvv;
            OrderId = orderId;
            Amount = amount;
        }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ValidDate { get; set; }
        public string Cvv { get; set; }

        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
