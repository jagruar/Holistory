using Holistory.Api.Application.Commands.Accounts.CreateAttempt;
using Holistory.Api.Application.Commands.Portal.Users.CreateUserCommand;
using Holistory.Api.Application.Commands.Portal.Users.GenerateAuthTokenCommand;
using Holistory.Api.Services.IdentityService;
using Holistory.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Holistory.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator, IIdentityService identityService)
            : base(mediator, identityService)
        {
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Token([FromBody] GenerateAuthTokenCommand command)
        {
            string token = await _Mediator.Send(command);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }
            else
            {
                return Ok(new { token });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Post([FromBody] CreateUserCommand command)
        {
            string result = await _Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("attempts")]
        [Authorize(Policy = IdentityRoles.User)]
        public async Task<ActionResult<int>> PostAttempt([FromBody] CreateAttemptCommand command)
        {
            if (command.UserId != _IdentityService.GetCurrentUserId())
            {
                return Forbid();
            }

            int result = await _Mediator.Send(command);
            return Ok(result);
        }
    }
}
