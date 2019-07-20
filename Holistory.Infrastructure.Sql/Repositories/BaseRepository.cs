using Holistory.Domain.Seedwork;
using Holistory.Infrastructure.Sql;
using System.Threading.Tasks;

namespace Holistory.Infrastructure.Sql.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : Entity, IAggregateRoot
    {
        protected readonly ApplicationDbContext _Context;

        public BaseRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public IUnitOfWork UnitOfWork => _Context;

        public virtual async Task<T> AddAsync(T entity)
        {
            await _Context.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public virtual void Delete(T entity)
        {
            _Context.Set<T>().Remove(entity);
        }
    }
}
