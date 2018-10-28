using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickForecaster.Application.Backlog.Commands;
using QuickForecaster.Application.Backlog.Queries;
using QuickForecaster.Application.Estimates.Commands;
using QuickForecaster.Application.Estimates.Queries;

namespace QuickForecaster.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacklogItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BacklogItemsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: api/backlogitems?estimateId=5
        [HttpGet("{estimateId}")]
        [ProducesResponseType(typeof(IEnumerable<BacklogItemDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetEstimates(int estimateId)
        {
            return Ok(await _mediator.Send(new GetBacklogItemsQuery(estimateId)));
        }

        // POST: api/backlogitems
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBackloItemCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT: api/backlogitems
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateBackloItemCommand command)
        {
            if (id != command.BacklogItemId)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));
        }

        // DELETE: api/backlogitems/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteBacklogItemCommand { Id = id });
            return NoContent();
        }
    }
}