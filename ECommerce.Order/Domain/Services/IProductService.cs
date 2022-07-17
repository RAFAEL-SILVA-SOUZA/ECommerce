using ECommerce.Order.Domain.Entities;

namespace ECommerce.Order.Domain.Services
{
    public interface IProductService
    {
        Task<IList<Product>> GetProducts(Guid[] ids);
    }
}
