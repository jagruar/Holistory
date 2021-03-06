﻿using Holistory.Api.Application.Commands.Topics.CreateAttempt;
using Holistory.Api.Application.Commands.Topics.CreateEvent;
using Holistory.Api.Application.Commands.Topics.CreateQuestion;
using Holistory.Api.Application.Commands.Topics.CreateTopic;
using Holistory.Api.DataTranserObjects;
using Holistory.Api.Queries.Interfaces;
using Holistory.Api.Services.IdentityService;
using Holistory.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holistory.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TopicsController : BaseController
    {
        private readonly ITopicQueries _topicQueries;

        public TopicsController(IIdentityService identityService, IMediator mediator, ITopicQueries topicQueries)
            : base(mediator, identityService)
        {
            _topicQueries = topicQueries;
        }

        [HttpGet]
        [Authorize(Policy = IdentityRoles.User)]
        public async Task<ActionResult<IEnumerable<TopicDto>>> Get()
        {
            IEnumerable<TopicDto> topics = await _topicQueries.GetAsync(_IdentityService.GetCurrentUserId());
            return Ok(topics);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = IdentityRoles.User)]
        public async Task<ActionResult<TopicDto>> Get([FromRoute] int id)
        {
            TopicDto topic = await _topicQueries.GetByIdAsync(_IdentityService.GetCurrentUserId(), id);
            return Ok(topic);
        }

        [HttpPost]
        [Authorize(Policy = IdentityRoles.Admin)]
        public async Task<ActionResult<int>> Post([FromBody] CreateTopicCommand command)
        {
            int result = await _Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPost("{id}/questions")]
        [Authorize(Policy = IdentityRoles.Admin)]
        public async Task<ActionResult<int>> PostQuestion([FromRoute] int id, [FromBody] CreateQuestionCommand command)
        {
            if (id != command.TopicId)
            {
                return BadRequestNonMatchingIds();
            }

            int result = await _Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPost("{id}/events")]
        [Authorize(Policy = IdentityRoles.Admin)]
        public async Task<ActionResult<int>> PostEvent([FromRoute] int id, [FromBody] CreateEventCommand command)
        {
            if (id != command.TopicId)
            {
                return BadRequestNonMatchingIds();
            }

            int result = await _Mediator.Send(command);
            return Ok(new { result });
        }

        [HttpPost("{id}/attempts")]
        [Authorize(Policy = IdentityRoles.User)]
        public async Task<ActionResult<AttemptDto>> PostAttempt([FromRoute] int id, [FromBody] CreateAttemptCommand command)
        {
            if (id != command.TopicId)
            {
                return BadRequestNonMatchingIds();
            }

            if (command.UserId != _IdentityService.GetCurrentUserId())
            {
                return Forbid();
            }

            AttemptDto result = await _Mediator.Send(command);
            return Ok(result);
        }
    }
}
