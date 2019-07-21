using Holistory.Common.Constants;
using Holistory.Domain.Aggregates.UserAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Holistory.Api.Services.IdentityService
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<User> _userManager;

        public IdentityService(IHttpContextAccessor context, UserManager<User> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public string GetCurrentUserId()
        {
            return _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        }

        public async Task<User> GetCurrentUserAsync()
        {
            return await _userManager.FindByIdAsync(GetCurrentUserId());
        }
    }
}
