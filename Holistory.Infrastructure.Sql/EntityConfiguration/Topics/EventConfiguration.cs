using Holistory.Domain.Aggregates.TopicAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holistory.Infrastructure.Sql.EntityConfiguration.Topics
{
    public class EventConfiguration : EntityConfigurationBase<Event>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Event> builder)
        {
            builder.HasOne<EventType>().WithMany().HasForeignKey(x => x.EventTypeId);
        }
    }
}
