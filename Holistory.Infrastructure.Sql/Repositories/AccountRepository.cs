using Holistory.Domain.Aggregates.AccountAggregate;

namespace Holistory.Infrastructure.Sql.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
