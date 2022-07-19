namespace ECommerce.Order.Models.Request
{
    public class PurchaseOrderItemCreateRequest
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }

    }
}
