using FluentValidation.Results;
using Holistory.Api.Services.IdentityService;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Holistory.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public const string ID_MUST_MATCH_COMMAND = "The request route ID must must match the command resource ID.";

        protected readonly IMediator _Mediator;
        protected readonly IIdentityService _IdentityService;

        public BaseController(IMediator mediator, IIdentityService identityService)
        {
            _IdentityService = identityService;
            _Mediator = mediator;
        }

        [NonAction]
        public BadRequestObjectResult BadRequestNonMatchingIds()
        {
            ProblemDetails problemDetails = new ProblemDetails()
            {
                Title = ID_MUST_MATCH_COMMAND,
                Status = (int)HttpStatusCode.BadRequest,
                Instance = $"{HttpContext.Request.Method} {HttpContext.Request.Path}",
            };

            return base.BadRequest(JsonConvert.SerializeObject(problemDetails));
        }

        [NonAction]
        public BadRequestObjectResult BadRequest(ValidationResult validationResult)
        {
            Dictionary<string, string[]> errors = new Dictionary<string, string[]>();
            validationResult.Errors.GroupBy(x => x.PropertyName).ToList().ForEach(x => errors.Add(x.Key, x.Select(c => c.ErrorMessage).ToArray()));

            ValidationProblemDetails problemDetails = new ValidationProblemDetails(errors)
            {
                Title = "One or more validation errors occurred.",
                Status = (int)HttpStatusCode.BadRequest,
                Instance = $"{HttpContext.Request.Method} {HttpContext.Request.Path}",
            };

            return base.BadRequest(JsonConvert.SerializeObject(problemDetails));
        }
    }
}
