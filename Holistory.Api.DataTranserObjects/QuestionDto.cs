using System.Collections.Generic;

namespace Holistory.Api.DataTranserObjects
{
    public class QuestionDto
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public int? EventId { get; set; }

        public string Text { get; set; }

        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}
