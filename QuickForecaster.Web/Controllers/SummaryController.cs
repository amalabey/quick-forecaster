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
    public class SummaryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SummaryController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EstimateSummaryDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<EstimateSummaryDto>> GetSummary([FromQuery]int estimateId, [FromQuery]int teamSize)
        {
            return Ok(await _mediator.Send(new GetSummaryQuery(estimateId, teamSize)));
        }
    }
}