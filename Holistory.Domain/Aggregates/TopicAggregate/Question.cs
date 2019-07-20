using Holistory.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Holistory.Domain.Aggregates.TopicAggregate
{
    public class Question : Entity
    {
        private readonly List<Answer> _answers = new List<Answer>();

        private Question()
        {
        }

        public Question(int? eventId, string text)
        {
            EventId = eventId;
            Text = text;
        }

        public int TopicId { get; private set; }

        public int? EventId { get; private set; }

        public string Text { get; private set; }

        public IReadOnlyCollection<Answer> Answers => _answers;

        public Answer AddAnswer(string text, bool isCorrect)
        {
            Answer answer = new Answer(text, isCorrect);
            _answers.Add(answer);
            return answer;
        }
    }
}
