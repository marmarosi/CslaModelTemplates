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
    /// Gets e new team to edit.
    /// </summary>
    [Route(Routes.Complex)]
    public class New : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<TeamDto>
    {
        internal ILogger Logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public New(
            ILogger<New> logger
            )
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets e new team to edit.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A new team..</returns>
        [HttpGet("new")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets e new team to edit.",
            Description = "Gets e new team to edit.<br>" +
                "Result: TeamDto",
            OperationId = "Team.New",
            Tags = new[] { "Complex Endpoints" })
        ]
        public override async Task<ActionResult<TeamDto>> HandleAsync(
            CancellationToken cancellationToken
            )
        {
            try
            {
                Team team = await Team.Create();
                return Ok(team.ToDto());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, Logger, ex);
            }
        }
    }
}
