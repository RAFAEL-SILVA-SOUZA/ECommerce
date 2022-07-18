using DotNetCore.CAP;
using ECommerce.Catalog.Domain.Entities;
using ECommerce.Catalog.Infra;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.Domain.Consumer
{
    public interface IProductConsumer
    {
        Task ProccessMessage(ProductStock[] productStock);
    }
    public class ProductConsumer : IProductConsumer, ICapSubscribe
    {
        private readonly CatalogDBContext _catalogDbContext;

        public ProductConsumer(CatalogDBContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        [CapSubscribe("ecomerce.catalog.stock")]
        public async Task ProccessMessage(ProductStock[] productsStock)
        {
            var ids = productsStock.Select(p => p.ItemId).ToArray();
            var products = await _catalogDbContext
                .Products
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            foreach (var product in products)
            {
                var stockProduct = productsStock.Single(x => x.ItemId == product.Id);
                product.Quantity -= stockProduct.Quantity;
            }

            await _catalogDbContext.SaveChangesAsync();
        }
    }
}
