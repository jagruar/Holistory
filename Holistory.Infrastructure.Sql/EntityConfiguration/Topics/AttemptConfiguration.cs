using Holistory.Domain.Aggregates.TopicAggregate;
using Holistory.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holistory.Infrastructure.Sql.EntityConfiguration.Topics
{
    public class AttemptConfiguration : EntityConfigurationBase<Attempt>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Attempt> builder)
        {
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
