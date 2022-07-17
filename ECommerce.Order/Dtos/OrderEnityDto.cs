using ECommerce.Order.Domain.Entities;

namespace ECommerce.Order.Dtos
{
    public class OrderEnityDto
    {
        public OrderEnityDto(OrderEntity entity)
        {
            Id = entity.Id;
            TotalAmount = entity.TotalAmount;
            CreatedAt = entity.CreatedAt;
            AddItens(entity.Itens.ToList());
        }

        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<OrderItemEnityDto> Itens { get; set; } = new List<OrderItemEnityDto>();

        private void AddItens(IList<OrderItem> itens)
        {
            foreach (var item in itens)
                Itens.Add(new OrderItemEnityDto(item));
        }
    }
}
