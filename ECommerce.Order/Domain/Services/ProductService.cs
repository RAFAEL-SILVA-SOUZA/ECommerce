using ECommerce.Order.Domain.Entities;
using Flurl;
using Flurl.Http;

namespace ECommerce.Order.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly Url _baseUrl;
        public ProductService(IConfiguration configuration)
        {
            _baseUrl = configuration["Catalog:url"];
        }

        public async Task<IList<Product>> GetProducts(Guid[] ids)
        {
            var request = _baseUrl
                .AppendPathSegment("/ids")
                .SetQueryParam("ids", ids);
            return await request.GetJsonAsync<Product[]>();
        }
    }
}
