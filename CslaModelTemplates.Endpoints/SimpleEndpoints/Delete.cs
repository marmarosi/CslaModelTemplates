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
    /// Deletes the specified team.
    /// </summary>
    [Route(Routes.Simple)]
    public class Delete : BaseAsyncEndpoint
        .WithRequest<SimpleTeamCriteria>
        .WithoutResponse
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Delete(
            ILogger<Delete> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Deletes the specified team.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Deletes the specified team.",
            Description = "Deletes the specified team. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamKey: number" +
                "<br>}",
            OperationId = "SimpleTeam.Delete",
            Tags = new[] { "Simple Endpoints" })
        ]
        public override async Task<ActionResult> HandleAsync(
            [FromQuery] SimpleTeamCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                await Task.Run(() => SimpleTeam.Delete(criteria));
                return NoContent();
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
