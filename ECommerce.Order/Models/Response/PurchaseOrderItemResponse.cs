using ECommerce.Order.Domain.Entities;

namespace ECommerce.Order.Models.Response;

public class PurchaseOrderItemResponse
{
    public PurchaseOrderItemResponse(PurchaseOrderItem item)
    {
        Id = item.Id;
        Quantity = item.Quantity;
        CatalogItem = new CatalogItemResponse(item.ProductId, item.Description, item.Price);
        TotalAmount = item.Quantity * item.Price;
    }

    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public CatalogItemResponse CatalogItem { get; set; }
}