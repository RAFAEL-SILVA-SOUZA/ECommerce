using ECommerce.Catalog.Domain.Entities;
using ECommerce.Catalog.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CatalogDBContext _catalogDbContext;

        public ProductController(CatalogDBContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _catalogDbContext.Products.AsNoTracking().ToListAsync();
        }

        [HttpGet("{ids}")]
        public async Task<IEnumerable<Product>> Get([FromQuery(Name = "ids")] Guid[] ids)
        {
            return await _catalogDbContext.Products
                .Where(x=>ids.Contains(x.Id))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
