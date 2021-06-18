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
    /// Gets a new team to edit.
    /// </summary>
    [Route(Routes.Simple)]
    public class New : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<SimpleTeamDto>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public New(
            ILogger<New> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets a new team to edit.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A new team..</returns>
        [HttpGet("new")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets a new team to edit.",
            Description = "Gets a new team to edit.<br><br>" +
                "Result: SimpleTeamDto",
            OperationId = "SimpleTeam.New",
            Tags = new[] { "Simple Endpoints" })
        ]
        public override async Task<ActionResult<SimpleTeamDto>> HandleAsync(
            CancellationToken cancellationToken
            )
        {
            try
            {
                SimpleTeam team = await SimpleTeam.Create();
                return Ok(team.ToDto<SimpleTeamDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
