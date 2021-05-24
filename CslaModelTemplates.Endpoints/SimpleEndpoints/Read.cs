using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Models.Simple;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.SimpleEndpoints
{
    /// <summary>
    /// Gets the specified team to edit.
    /// </summary>
    [Route(Routes.Simple)]
    public class Read : BaseAsyncEndpoint
        .WithRequest<SimpleTeamCriteria>
        .WithResponse<SimpleTeamDto>
    {
        internal ILogger logger { get; set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Read(
            ILogger logger
            )
        {
            this.logger = logger;
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
            Description = "Gets the specified team details to display. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamKey: long" +
                "<br>}<br>" +
                "Result: SimpleTeamDto",
            OperationId = "SimpleTeam.Read",
            Tags = new[] { "Simple Endpoints" })
        ]
        public override async Task<ActionResult<SimpleTeamDto>> HandleAsync(
            [FromQuery] SimpleTeamCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                SimpleTeam team = await SimpleTeam.Get(criteria);
                return Ok(team.ToDto<SimpleTeamDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}