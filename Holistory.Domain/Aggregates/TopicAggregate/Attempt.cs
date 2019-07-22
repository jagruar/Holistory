using Holistory.Domain.Seedwork;
using System;

namespace Holistory.Domain.Aggregates.TopicAggregate
{
    public class Attempt : Entity
    {
        private Attempt()
        {
        }

        public Attempt(string userId, int correct, int incorrect)
        {
            UserId = userId;
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
