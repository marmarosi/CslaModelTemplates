using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.ComplexSet;
using CslaModelTemplates.Models.ComplexSet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.ComplexEndpoints
{
    /// <summary>
    /// Updates the specified team set.
    /// </summary>
    [Route(Routes.Complex)]
    public class UpdateSet : BaseAsyncEndpoint
        .WithRequest<TeamSetRequest>
        .WithResponse<IList<TeamSetItemDto>>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public UpdateSet(
            ILogger<UpdateSet> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Updates the specified team set.
        /// </summary>
        /// <param name="request">The input data to update a team set.</param>
        /// <param name="dto">The data transer objects of the team set.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated team.</returns>
        [HttpPut("set")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Updates the specified team set.",
            Description = "Updates the specified team set Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamName: string" +
                "<br>}<br>" +
                "Data: TeamSetItemDto[]<br>" +
                "Result: TeamSetItemDto[]",
            OperationId = "TeamSet.Update",
            Tags = new[] { "Complex Endpoints" })
        ]
        public override async Task<ActionResult<IList<TeamSetItemDto>>> HandleAsync(
            [FromRoute] TeamSetRequest request,
            CancellationToken cancellationToken
            )
        {
            try
            {
                TeamSet team = await TeamSet.FromDto(request.Criteria, request.Dto);
                if (team.IsSavable)
                {
                    team = await team.SaveAsync();
                }
                return Ok(team.ToDto<TeamSetItemDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }

    /// <summary>
    /// Defines the input data to update a team set.
    /// </summary>
    public class TeamSetRequest
    {
        /// <summary>
        /// The criteria of the team set.
        /// </summary>
        [FromQuery] public TeamSetCriteria Criteria { get; set; }

        /// <summary>
        /// The data transer objects of the team set.
        /// </summary>
        [FromBody] public List<TeamSetItemDto> Dto { get; set; }
    }
}
