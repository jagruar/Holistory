using Holistory.Domain.Aggregates.TopicAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holistory.Infrastructure.Sql.EntityConfiguration.Topics
{
    public class QuestionConfiguration : EntityConfigurationBase<Question>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Question> builder)
        {
            builder.HasMany(x => x.Answers).WithOne().HasForeignKey(x => x.QuestionId);

            builder.OwnsMany(x => x.Answers, y =>
            {
                y.HasKey(z => z.Id);
                y.HasForeignKey(z => z.QuestionId);
                y.Property(z => z.Text).IsRequired();
            });

            builder.HasOne<Event>().WithMany().HasForeignKey(x => x.EventId);

            builder.Metadata.FindNavigation(nameof(Question.Answers)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
