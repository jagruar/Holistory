using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace Holistory.Infrastructure.Sql.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder DisableCascadingDeletes(this ModelBuilder builder)
        {
            IEnumerable<IMutableForeignKey> cascadeFKs = builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (IMutableForeignKey fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            return builder;
        }
    }
}
