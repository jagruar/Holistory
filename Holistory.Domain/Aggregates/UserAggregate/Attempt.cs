using Holistory.Domain.Seedwork;
using System;

namespace Holistory.Domain.Aggregates.UserAggregate
{
    public class Attempt : Entity
    {
        private Attempt()
        {
        }

        public Attempt(int topicId, int correct, int incorrect)
        {
            TopicId = topicId;
            DateTaken = DateTime.Now;
            Correct = correct;
            Incorrect = incorrect;
        }

        public string UserId { get; private set; }

        public int TopicId { get; private set; }

        public DateTime DateTaken { get; private set; }

        public int Correct { get; private set; }

        public int Incorrect { get; private set; }
    }
}
