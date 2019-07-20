using System;
using System.Collections.Generic;
using Holistory.Domain.Seedwork;

namespace Holistory.Domain.Aggregates.AccountAggregate
{
    public class Account : Entity, IAggregateRoot
    {
        private readonly List<AccountTopic> _topics = new List<AccountTopic>();

        private Account()
        {
        }

        public Account(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }

        public IReadOnlyCollection<AccountTopic> Topics => _topics;

        public AccountTopic AddTopic(int topicId, int correct, int incorrect)
        {
            AccountTopic accountTopic = new AccountTopic(topicId, correct, incorrect);
            _topics.Add(accountTopic);
            return accountTopic;
        }
    }
}
