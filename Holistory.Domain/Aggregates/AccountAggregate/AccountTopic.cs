using Holistory.Domain.Seedwork;
using System;

namespace Holistory.Domain.Aggregates.AccountAggregate
{
    public class AccountTopic : Entity
    {
        private AccountTopic()
        {
        }

        public AccountTopic(int topicId, int correct, int incorrect)
        {
            TopicId = topicId;
            DateTaken = DateTime.Now;
            Correct = correct;
            Incorrect = incorrect;
        }

        public int AccountId { get; private set; }

        public int TopicId { get; private set; }

        public DateTime DateTaken { get; private set; }

        public int Correct { get; private set; }

        public int Incorrect { get; private set; }
    }
}
