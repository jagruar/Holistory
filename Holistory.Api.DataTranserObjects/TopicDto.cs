using System.Collections.Generic;
using System.Linq;

namespace Holistory.Api.DataTranserObjects
{
    public class TopicDto
    {
        public int Id { get; set; }

        public int EraId { get; set; }

        public int RegionId { get; set; }

        public IEnumerable<EventDto> Events { get; set; }

        public IEnumerable<AttemptDto> Attempts { get; set; }

        public IEnumerable<QuestionDto> Questions { get; set; }

        public TopicStatus Status { get; set; }

        public void SetStatus()
        {
            if (Attempts.Any())
            {
                if (Attempts.Any(x => x.Incorrect == 0))
                {
                    Status = TopicStatus.Completed;
                }
                else
                {
                    Status = TopicStatus.Attempted;
                }
            }
            else
            {
                Status = TopicStatus.NotAttempted;
            }
        }
    }
}
