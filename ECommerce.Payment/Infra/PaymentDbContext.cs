using ECommerce.Payment.Models;
using Microsoft.EntityFrameworkCore;

using PaymentModel = ECommerce.Payment.Domain.Entities.Payment;

namespace ECommerce.Payment.Infra
{
    public class PaymentDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<PaymentModel> Payments { get; set; }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> contextOptions,
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
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PaymentConnection"));
        }
    }
}
