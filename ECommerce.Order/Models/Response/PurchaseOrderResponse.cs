using ECommerce.Order.Domain.Entities;
using ECommerce.Order.Domain.Entities.Enums;

namespace ECommerce.Order.Models.Response
{
    public class PurchaseOrderResponse
    {
        public PurchaseOrderResponse(PurchaseOrder entity)
        {
            Id = entity.Id;
            TotalAmount = entity.TotalAmount;
            CreatedAt = entity.CreatedAt;
            OrderStatus = entity.OrderStatus;
            AddOrderItem(entity.PurchaseOrderItems.ToList());
        }

        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public IList<PurchaseOrderItemResponse> Items { get; set; } = new List<PurchaseOrderItemResponse>();

        private void AddOrderItem(IEnumerable<PurchaseOrderItem> orderItems)
        {
            foreach (var orderItem in orderItems)
                Items.Add(new PurchaseOrderItemResponse(orderItem));
        }
    }
}
