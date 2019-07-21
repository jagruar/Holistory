using Holistory.Api.DataTranserObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holistory.Api.Queries.Interfaces
{
    public interface ITopicQueries
    {
        /// <summary>
        /// Retrieves a list of topics including attempts made by the given account
        /// </summary>
        Task<IEnumerable<TopicDto>> GetAsync(string userId);

        /// <summary>
        /// Retrieves a topic including events and questions
        /// </summary>
        Task<TopicDto> GetByIdAsync(int topicId);
    }
}
