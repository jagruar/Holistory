using Holistory.Domain.Seedwork;

namespace Holistory.Domain.Aggregates.TopicAggregate
{
    public class Answer
    {
        private Answer()
        {
        }

        public Answer(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
        }

        public int Id { get; private set; }

        public int QuestionId { get; private set; }

        public string Text { get; private set; }

        public bool IsCorrect { get; private set; }
    }
}
