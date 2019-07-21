using Holistory.Domain.Aggregates.UserAggregate;
using System.Threading.Tasks;

namespace Holistory.Api.Services.IdentityService
{
    public interface IIdentityService
    {
        string GetCurrentUserId();

        Task<User> GetCurrentUserAsync();
    }
}
