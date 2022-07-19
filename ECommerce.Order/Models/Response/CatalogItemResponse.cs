namespace ECommerce.Order.Models.Response
{
    public class CatalogItemResponse
    {
        public CatalogItemResponse(Guid id, string description, decimal price)
        {
            Id = id;
            Description = description;
            Price = price;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
