using Holistory.Domain.Seedwork;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Holistory.Domain.Aggregates.UserAggregate
{
    public class User : IdentityUser, IAggregateRoot
    {
        private readonly List<Attempt> _attempts = new List<Attempt>();

        private User()
        {
        }

        public User(string username, string email) :
            base(username)
        {
            Email = email;
        }

        public IReadOnlyCollection<Attempt> Attempts => _attempts;

        public Attempt AddAttempt(int topicId, int correct, int incorrect)
        {
            Attempt attempt = new Attempt(topicId, correct, incorrect);
            _attempts.Add(attempt);
            return attempt;
        }
    }
}
