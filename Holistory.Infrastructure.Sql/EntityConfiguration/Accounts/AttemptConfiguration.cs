using Holistory.Domain.Aggregates.UserAggregate;
using Holistory.Domain.Aggregates.TopicAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holistory.Infrastructure.Sql.EntityConfiguration.Accounts
{
    public class AttemptConfiguration : EntityConfigurationBase<Attempt>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Attempt> builder)
        {
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne<Topic>().WithMany().HasForeignKey(x => x.TopicId);
        }
    }
}
