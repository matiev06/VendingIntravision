
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingIntravision.ApplicationCore.Entities.CoinInventoryAggregate;

namespace Infrastructure.Data.Config;

public class CoinInventoryConfiguration : IEntityTypeConfiguration<CoinInventory>
{
    public void Configure(EntityTypeBuilder<CoinInventory> builder)
    {
        builder.Property(c => c.CoinValue)
            .IsRequired();

        builder.Property(q => q.Quantity)
            .IsRequired();

        builder.Property(ib => ib.IsBlocked)
            .HasDefaultValue(false);
    }
}
