using Holistory.Api.Application.Commands.Topics.CreateAttempt;
using Holistory.Api.Application.Commands.Portal.Users.CreateUserCommand;
using Holistory.Api.Application.Commands.Portal.Users.GenerateAuthTokenCommand;
using Holistory.Api.Services.IdentityService;
using Holistory.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Holistory.Api.DataTranserObjects;

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
        public async Task<ActionResult<IdentificationDto>> Token([FromBody] GenerateAuthTokenCommand command)
        {
            IdentificationDto result = await _Mediator.Send(command);

            if (string.IsNullOrEmpty(result.Token))
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Post([FromBody] CreateUserCommand command)
        {
            string result = await _Mediator.Send(command);
            return Ok(result);
        }
    }
}
