using ECommerce.Order.Domain.Entities.Enums;

namespace ECommerce.Order.Domain.Entities
{
    public class OrderEntity
    {
        public static OrderEntity Instance = new()
        {
            CreatedAt = DateTime.Now
        };

        public OrderEntity()
        {
            Itens = new HashSet<OrderItem>();
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;
        public Decimal TotalAmount { get; set; }
        public ICollection<OrderItem> Itens { get; set; }


        public void AddItens(OrderItem orderItem)
        {
            Itens.Add(orderItem);
            SetTotalAmount();
        }

        public void SetTotalAmount()
        {
            TotalAmount = Itens.Sum(x => x.Price);
        }
    }
}
