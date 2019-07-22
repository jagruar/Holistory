using Holistory.Domain.Aggregates.TopicAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Holistory.Infrastructure.Sql.EntityConfiguration.Topics
{
    public class TopicConfiguration : EntityConfigurationBase<Topic>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Topic> builder)
        {
            builder.HasMany(x => x.Events).WithOne().HasForeignKey(x => x.TopicId);
            builder.HasMany(x => x.Questions).WithOne().HasForeignKey(x => x.TopicId);
            builder.HasMany(x => x.Attempts).WithOne().HasForeignKey(x => x.TopicId);

            builder.HasOne<Era>().WithMany().HasForeignKey(x => x.EraId);
            builder.HasOne<Region>().WithMany().HasForeignKey(x => x.RegionId);

            builder.Metadata.FindNavigation(nameof(Topic.Questions)).SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Metadata.FindNavigation(nameof(Topic.Events)).SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Metadata.FindNavigation(nameof(Topic.Attempts)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
