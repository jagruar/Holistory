using Holistory.Api.Application.Commands.Accounts.CreateAccountTopic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Holistory.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController : BaseController
    {
        public AccountsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("{id}/topics")]
        public async Task<ActionResult<int>> PostAccountTopic([FromRoute] int id, [FromBody] CreateAccountTopicCommand command)
        {
            int result = await _Mediator.Send(command);
            return Ok(result);
        }
    }
}
