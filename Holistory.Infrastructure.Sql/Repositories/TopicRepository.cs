using System.Threading.Tasks;
using Holistory.Domain.Aggregates.TopicAggregate;
using Microsoft.EntityFrameworkCore;

namespace Holistory.Infrastructure.Sql.Repositories
{
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async override Task<Topic> GetAsync(int id)
        {
            return await _Context.Topic
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .Include(x => x.Events)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
