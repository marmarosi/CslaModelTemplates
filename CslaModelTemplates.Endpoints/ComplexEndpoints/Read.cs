using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.Complex;
using CslaModelTemplates.Models.Complex;
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
    /// Gets the specified team to edit.
    /// </summary>
    [Route(Routes.Complex)]
    public class Read : BaseAsyncEndpoint
        .WithRequest<TeamParams>
        .WithResponse<TeamDto>
    {
        internal ILogger Logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Read(
            ILogger<Read> logger
            )
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the specified team to edit.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The requested team.</returns>
        [HttpGet("read")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the specified team to edit.",
            Description = "Gets the specified team details to edit. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamKey: number" +
                "<br>}<br>" +
                "Result: TeamDto",
            OperationId = "Team.Read",
            Tags = new[] { "Complex Endpoints" })
        ]
        public override async Task<ActionResult<TeamDto>> HandleAsync(
            [FromQuery] TeamParams criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                Team team = await Team.Get(criteria);
                return Ok(team.ToDto());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, Logger, ex);
            }
        }
    }
}
