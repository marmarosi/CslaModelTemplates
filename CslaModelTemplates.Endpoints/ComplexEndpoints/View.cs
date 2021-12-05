using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.ComplexView;
using CslaModelTemplates.Models.ComplexView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.ComplexEndpoints
{
    /// <summary>
    /// Gets the specified team details to display.
    /// </summary>
    [Route(Routes.Complex)]
    public class View : BaseAsyncEndpoint
        .WithRequest<TeamViewParams>
        .WithResponse<TeamViewDto>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public View(
            ILogger<View> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the specified team details to display.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The requested team view.</returns>
        [HttpGet("view")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the specified team details to display.",
            Description = "Gets the specified team details to display.<br>" +
                "Criteria:<br>{<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;teamId: string<br>" +
                "}<br>" +
                "Result: TeamViewDto",
            OperationId = "Team.View",
            Tags = new[] { "Complex Endpoints" })
        ]
        public override async Task<ActionResult<TeamViewDto>> HandleAsync(
            [FromQuery] TeamViewParams criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                TeamView team = await TeamView.Get(criteria);
                return Ok(team.ToDto<TeamViewDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
