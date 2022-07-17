namespace ECommerce.Order.Domain.Entities;

public class OrderItem
{
    public OrderItem(string description, decimal price)
    {
        Description = description;
        Price = price;
    }

    public Guid Id { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public OrderEntity OrderEntity { get; set; }
    public Guid OrderEntityId { get; set; }
}