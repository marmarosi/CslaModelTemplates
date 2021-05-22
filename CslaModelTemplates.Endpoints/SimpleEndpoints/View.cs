using CslaModelTemplates.Contracts.SimpleView;
using CslaModelTemplates.Models.SimpleView;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.SimpleEndpoints
{
    /// <summary>
    /// Gets the specified team details to display.
    /// </summary>
    public class View : SimpleAsyncEndpoint<SimpleTeamViewCriteria, SimpleTeamViewDto>
    {
        /// <summary>
        /// Gets the specified team details to display.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of teams.</returns>
        [HttpGet("view")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the specified team details to display.",
            Description = "Gets the specified team details to display. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamKey: long" +
                "<br>}<br>" +
                "Result: SimpleTeamViewDto",
            OperationId = "SimpleTeam.View",
            Tags = new[] { "Simple Endpoints" })
        ]
        public override async Task<ActionResult<SimpleTeamViewDto>> HandleAsync(
            [FromQuery] SimpleTeamViewCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                SimpleTeamView team = await SimpleTeamView.Get(criteria);
                return Ok(team.ToDto<SimpleTeamViewDto>());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
