using System.Threading.Tasks;

namespace Holistory.Domain.Seedwork
{
    public interface IRepository<T>
        where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task<T> GetAsync(int id);

        Task<T> AddAsync(T entity);
    }
}