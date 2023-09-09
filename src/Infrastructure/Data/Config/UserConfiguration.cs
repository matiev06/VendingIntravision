using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingIntravision.ApplicationCore.Entities.UserAggregate;

namespace Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(idd => idd.IdIdentity)
            /*.IsRequired()*/;

        builder.Property(b => b.Balance)
            .IsRequired();

        builder.Property(b => b.Guid)
            .IsRequired();
    }
}
