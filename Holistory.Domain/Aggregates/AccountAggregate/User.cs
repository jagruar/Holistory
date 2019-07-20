using Holistory.Domain.Seedwork;
using Microsoft.AspNetCore.Identity;

namespace Holistory.Domain.Aggregates.AccountAggregate
{
    public class User : IdentityUser
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
