using Holistory.Domain.Aggregates.UserAggregate;
using Holistory.Domain.Seedwork;
using System.Threading.Tasks;

namespace Holistory.Infrastructure.Sql.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<User> GetAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
