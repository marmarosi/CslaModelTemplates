using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.ComplexCommand;
using CslaModelTemplates.Models.ComplexCommand;
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
    /// Counts the teams grouped by the number of their items.
    /// </summary>
    [Route(Routes.Complex)]
    public class Command : BaseAsyncEndpoint
        .WithRequest<CountTeamsCriteria>
        .WithResponse<CountTeamsListItemDto>
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
        /// Counts the teams grouped by the number of their items.
        /// </summary>
        /// <param name="dto">The data transer object of the rename team command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True when the team was renamed; otherwise false.</returns>
        [HttpPatch]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Counts the teams grouped by the number of their items.",
            Description = "Counts the teams grouped by the number of their items.<br>" +
                "Criteria:<br>{<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;teamName: string<br>" +
                "}<br>" +
                "Result: CountTeamsListItemDto",
            OperationId = "SimpleTeam.Rename",
            Tags = new[] { "Complex Endpoints" })
        ]
        public override async Task<ActionResult<CountTeamsListItemDto>> HandleAsync(
            [FromBody] CountTeamsCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                CountTeamsList list = await CountTeams.Execute(criteria);
                return Ok(list.ToDto<CountTeamsListItemDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
