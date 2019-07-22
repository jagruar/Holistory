using Holistory.Domain.Seedwork;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Holistory.Domain.Aggregates.UserAggregate
{
    public class User : IdentityUser, IAggregateRoot
    {
        private User()
        {
        }

        public User(string username, string email) :
            base(username)
        {
            Email = email;
        }
    }
}
