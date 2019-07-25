using Holistory.Domain.Seedwork;
using System.Collections.Generic;

namespace Holistory.Domain.Aggregates.TopicAggregate
{
    public class Question : Entity
    {
        private readonly List<Answer> _answers = new List<Answer>();

        private Question()
        {
        }

        public Question(List<Answer> answers, int? eventId, string text)
        {
            _answers = answers;
            EventId = eventId;
            Text = text;
        }

        public int TopicId { get; private set; }

        public int? EventId { get; private set; }

        public string Text { get; private set; }

        public IReadOnlyCollection<Answer> Answers => _answers;
    }
}
