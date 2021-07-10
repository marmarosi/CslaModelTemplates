using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.SimpleCommand;
using CslaModelTemplates.Models.SimpleCommand;
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
    /// Renames the specified team.
    /// </summary>
    [Route(Routes.Simple)]
    public class Command : BaseAsyncEndpoint
        .WithRequest<RenameTeamDto>
        .WithResponse<bool>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Command(
            ILogger<Command> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Renames the specified team.
        /// </summary>
        /// <param name="dto">The data transer object of the rename team command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True when the team was renamed; otherwise false.</returns>
        [HttpPatch]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Renames the specified team.",
            Description = "Renames the specified team.<br>" +
                "Data: RenameTeamDto<br>" +
                "Result: boolean",
            OperationId = "SimpleTeam.Rename",
            Tags = new[] { "Simple Endpoints" })
        ]
        public override async Task<ActionResult<bool>> HandleAsync(
            [FromBody] RenameTeamDto dto,
            CancellationToken cancellationToken
            )
        {
            try
            {
                return await Run.RetryOnDeadlock(async () =>
                {
                    bool result = await RenameTeam.Execute(dto);
                    return Ok(result);
                });
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
