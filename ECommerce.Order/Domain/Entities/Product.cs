﻿namespace ECommerce.Order.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
