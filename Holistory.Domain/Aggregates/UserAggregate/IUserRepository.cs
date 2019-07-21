using Holistory.Domain.Seedwork;
using System.Threading.Tasks;

namespace Holistory.Domain.Aggregates.UserAggregate
{
    public interface IUserRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<User> GetAsync(string id);
    }
}
