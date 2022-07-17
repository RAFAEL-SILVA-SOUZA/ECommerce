using ECommerce.Order.Domain.Entities.Enums;

namespace ECommerce.Order.Domain.Entities
{
    public class OrderEntity
    {

        public OrderEntity()
        {
            Itens = new HashSet<OrderItem>();

            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            OrderStatus = OrderStatus.Created;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public string GatewayName { get; set; }
        public Guid TranzactionId { get; set; }
        public ICollection<OrderItem> Itens { get; set; }


        public void AddItens(OrderItem orderItem)
        {
            Itens.Add(orderItem);
            SetTotalAmount(orderItem);
        }

        private void SetTotalAmount(OrderItem orderItem)
        {
            TotalAmount += orderItem.Price * orderItem.Quantity;
        }
    }
}
