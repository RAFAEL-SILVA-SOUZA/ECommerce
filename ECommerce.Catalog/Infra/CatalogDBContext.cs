using ECommerce.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.Infra
{
    public class CatalogDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Product> Products { get; set; }

        public CatalogDBContext(DbContextOptions<CatalogDBContext> contextOptions,
            IConfiguration configuration) : base(contextOptions)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CatalogConnection"));
        }
    }
}
