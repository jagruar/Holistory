using Holistory.Api.Application.Commands.Portal.Users.CreateUserCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Holistory.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateUserCommand command)
        {
            int result = await _Mediator.Send(command);
            return Ok(result);
        }
    }
}
