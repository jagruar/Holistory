using Holistory.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Holistory.Infrastructure.Sql.EntityConfiguration
{
    public abstract class EntityConfigurationBase<TBase> : IEntityTypeConfiguration<TBase>
        where TBase : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.HasQueryFilter(e => !e.UtcDateDeleted.HasValue);
            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<TBase> builder);
    }
}
