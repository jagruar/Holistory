using Holistory.Common.Constants;
using Holistory.Domain.Aggregates.TopicAggregate;
using Holistory.Domain.Seedwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Holistory.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ErasController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = IdentityRoles.User)]
        // [ResponseCache(Duration = 6000)]
        public ActionResult<IEnumerable<Era>> Get()
        {
            return Ok(Enumeration.GetAll<Era>().Select(x => new { x.Id, x.Name }));
        }
    }
}
