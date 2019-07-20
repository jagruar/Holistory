using Holistory.Domain.Aggregates.TopicAggregate;
using Holistory.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holistory.Infrastructure.Sql.EntityConfiguration.Topics
{
    public class EraConfiguration : IEntityTypeConfiguration<Era>
    {
        public void Configure(EntityTypeBuilder<Era> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder.HasData(Enumeration.GetAll<Era>());
        }
    }
}
