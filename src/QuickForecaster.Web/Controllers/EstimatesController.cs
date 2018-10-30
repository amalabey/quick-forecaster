using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickForecaster.Application.Estimates.Commands;
using QuickForecaster.Application.Estimates.Queries;

namespace QuickForecaster.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EstimatesController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{clientId}")]
        [ProducesResponseType(typeof(IEnumerable<ProjectDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetEstimates(int clientId)
        {
            return Ok(await _mediator.Send(new GetProjectsQuery(clientId)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEstimateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}