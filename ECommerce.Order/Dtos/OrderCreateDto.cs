namespace ECommerce.Order.Dtos
{
    public class OrderCreateDto
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ValidDate { get; set; }
        public string Cvv { get; set; }
        public ICollection<OrderItemCreateDto> Itens { get; set; }
    }
}
