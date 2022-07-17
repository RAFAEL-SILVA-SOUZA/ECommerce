using ECommerce.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Order.Infra
{
    public class OrderEntityConfig : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValue(Guid.NewGuid());
            builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasMany(x => x.Itens)
                .WithOne(x => x.OrderEntity);
        }
    }
}
