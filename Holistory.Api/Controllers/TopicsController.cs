using Holistory.Api.Application.Commands.Topics.CreateAnswer;
using Holistory.Api.Application.Commands.Topics.CreateEvent;
using Holistory.Api.Application.Commands.Topics.CreateQuestion;
using Holistory.Api.Application.Commands.Topics.CreateTopic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Holistory.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TopicsController : BaseController
    {
        public TopicsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateTopicCommand command)
        {
            int result = await _Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{id}/questions")]
        public async Task<ActionResult<int>> PostQuestion([FromRoute] int id, [FromBody] CreateQuestionCommand command)
        {
            if (id != command.TopicId)
            {
                return BadRequestNonMatchingIds();
            }

            int result = await _Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{id}/events")]
        public async Task<ActionResult<int>> PostQuestion([FromRoute] int id, [FromBody] CreateEventCommand command)
        {
            if (id != command.TopicId)
            {
                return BadRequestNonMatchingIds();
            }

            int result = await _Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{topicId}/questions/{questionId}/answers")]
        public async Task<ActionResult<int>> PostQuestion([FromRoute] int topicId, [FromRoute] int questionId, [FromBody] CreateAnswerCommand command)
        {
            if (topicId != command.TopicId || questionId != command.QuestionId)
            {
                return BadRequestNonMatchingIds();
            }

            int result = await _Mediator.Send(command);
            return Ok(result);
        }
    }
}
