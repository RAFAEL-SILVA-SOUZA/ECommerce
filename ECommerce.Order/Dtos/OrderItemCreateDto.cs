namespace ECommerce.Order.Dtos
{
    public class OrderItemCreateDto
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }

    }
}
