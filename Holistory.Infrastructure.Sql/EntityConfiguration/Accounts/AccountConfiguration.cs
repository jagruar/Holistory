using Holistory.Domain.Aggregates.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holistory.Infrastructure.Sql.EntityConfiguration.Accounts
{
    public class AccountConfiguration : EntityConfigurationBase<Account>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Account> builder)
        {
            builder.HasMany(x => x.Topics).WithOne().HasForeignKey(x => x.AccountId);

            builder.Metadata.FindNavigation(nameof(Account.Topics)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
