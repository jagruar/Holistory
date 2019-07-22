using System.Collections.Generic;
using System.Linq;

namespace Holistory.Api.DataTranserObjects
{
    public class TopicDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int EraId { get; set; }

        public int RegionId { get; set; }

        public IEnumerable<EventDto> Events { get; set; }

        public IEnumerable<AttemptDto> Attempts { get; set; }

        public IEnumerable<QuestionDto> Questions { get; set; }
    }
}
