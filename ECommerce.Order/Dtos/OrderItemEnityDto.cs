using ECommerce.Order.Domain.Entities;

namespace ECommerce.Order.Dtos;

public class OrderItemEnityDto
{
    public OrderItemEnityDto(OrderItem item)
    {
        Id = item.Id;
        Description = item.Description;
        Price = item.Price;
    }

    public Guid Id { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}