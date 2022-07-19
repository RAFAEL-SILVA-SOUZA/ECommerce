namespace ECommerce.Order.Models.Request
{
    public class PurchaseOrderReproccessRequest
    {
        public Guid Id { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ValidDate { get; set; }
        public string Cvv { get; set; }
        public ICollection<PurchaseOrderItemCreateRequest> PurchaseOrderItemsCreateRequest { get; set; }
    }
}
