using System;
using System.Threading;
using System.Threading.Tasks;

namespace Holistory.Domain.Seedwork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Publishes domain events and adds any required data persistence operations to the current database transaction.
        /// </summary>
        Task SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
