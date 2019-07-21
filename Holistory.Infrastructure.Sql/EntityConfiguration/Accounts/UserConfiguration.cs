using Holistory.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holistory.Infrastructure.Sql.EntityConfiguration.Accounts
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.Attempts).WithOne().HasForeignKey(x => x.UserId);

            builder.Metadata.FindNavigation(nameof(User.Attempts)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
