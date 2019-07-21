using System;

namespace Holistory.Api.DataTranserObjects
{
    public class AttemptDto
    {
        public int TopicId { get; set; }

        public DateTime DateTaken { get; set; }

        public int Correct { get; set; }

        public int Incorrect { get; set; }
    }
}
