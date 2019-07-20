using Holistory.Domain.Aggregates.AccountAggregate;
using Holistory.Domain.Aggregates.TopicAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holistory.Infrastructure.Sql.EntityConfiguration.Accounts
{
    public class AccountTopicConfiguration : EntityConfigurationBase<AccountTopic>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<AccountTopic> builder)
        {
            builder.HasOne<Topic>().WithMany().HasForeignKey(x => x.TopicId);
        }
    }
}
