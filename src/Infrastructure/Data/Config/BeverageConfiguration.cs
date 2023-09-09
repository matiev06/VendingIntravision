using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingIntravision.ApplicationCore.Entities.BeverageAggregate;

namespace Infrastructure.Data.Config;

public class BeverageConfiguration : IEntityTypeConfiguration<Beverage>
{
    public void Configure(EntityTypeBuilder<Beverage> builder)
    {
        builder.Property(n => n.Name)
            .IsRequired();

        builder.Property(p => p.Price)
            .IsRequired();

        builder.Property(q => q.Quantity)
            .IsRequired();
    }
}
