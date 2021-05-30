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
    /// Deletes the specified team.
    /// </summary>
    [Route(Routes.Complex)]
    public class Delete : BaseAsyncEndpoint
        .WithRequest<TeamCriteria>
        .WithoutResponse
    {
        internal ILogger logger { get; set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Delete(
            ILogger logger
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
            OperationId = "Team.Delete",
            Tags = new[] { "Complex Endpoints" })
        ]
        public override async Task<ActionResult> HandleAsync(
            [FromQuery] TeamCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                await Task.Run(() => Team.Delete(criteria));
                return NoContent();
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
