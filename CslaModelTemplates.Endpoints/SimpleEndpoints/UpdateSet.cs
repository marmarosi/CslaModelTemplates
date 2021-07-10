using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Models.SimpleSet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.SimpleEndpoints
{
    /// <summary>
    /// Updates the specified team set.
    /// </summary>
    [Route(Routes.Simple)]
    public class UpdateSet : BaseAsyncEndpoint
        .WithRequest<SimpleTeamSetRequest>
        .WithResponse<IList<SimpleTeamSetItemDto>>
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
                "Data: SimpleTeamSetItemDto[]<br>" +
                "Result: SimpleTeamSetItemDto[]",
            OperationId = "SimpleTeamSet.Update",
            Tags = new[] { "Simple Endpoints" })
        ]
        public override async Task<ActionResult<IList<SimpleTeamSetItemDto>>> HandleAsync(
            [FromRoute] SimpleTeamSetRequest request,
            CancellationToken cancellationToken
            )
        {
            try
            {
                return await Call<IList<SimpleTeamSetItemDto>>.RetryOnDeadlock(async () =>
                {
                    SimpleTeamSet team = await SimpleTeamSet.FromDto(request.Criteria, request.Dto);
                    if (team.IsSavable)
                    {
                        team = await team.SaveAsync();
                    }
                    return Ok(team.ToDto<SimpleTeamSetItemDto>());
                });
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
    public class SimpleTeamSetRequest
    {
        /// <summary>
        /// The criteria of the team set.
        /// </summary>
        [FromQuery] public SimpleTeamSetCriteria Criteria { get; set; }

        /// <summary>
        /// The data transer objects of the team set.
        /// </summary>
        [FromBody] public List<SimpleTeamSetItemDto> Dto { get; set; }
    }
}
