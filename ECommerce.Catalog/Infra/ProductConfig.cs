using Bogus;
using ECommerce.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Catalog.Infra
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValue(Guid.NewGuid());
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

            var productsFaker = new Faker<Product>("pt_BR")
                .RuleFor(x => x.Id, f => Guid.NewGuid())
                .RuleFor(x => x.Description, f => f.Commerce.Product())
                .RuleFor(x => x.Price, f => f.Random.Decimal(5, 500))
                .RuleFor(x => x.Quantity, f => 10);

            builder.HasData(productsFaker.Generate(10));
        }
    }
}
