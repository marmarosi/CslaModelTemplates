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
    /// Updates the specified team.
    /// </summary>
    [Route(Routes.Simple)]
    public class Update : BaseAsyncEndpoint
        .WithRequest<SimpleTeamDto>
        .WithResponse<SimpleTeamDto>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Update(
            ILogger<Update> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Updates the specified team.
        /// </summary>
        /// <param name="dto">The data transer object of the team.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated team.</returns>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Updates the specified team.",
            Description = "Updates the specified team.<br>" +
                "Data: SimpleTeamDto<br>" +
                "Result: SimpleTeamDto",
            OperationId = "SimpleTeam.Update",
            Tags = new[] { "Simple Endpoints" })
        ]
        public override async Task<ActionResult<SimpleTeamDto>> HandleAsync(
            [FromBody] SimpleTeamDto dto,
            CancellationToken cancellationToken
            )
        {
            try
            {
                return await Call<SimpleTeamDto>.RetryOnDeadlock(async () =>
                {
                    SimpleTeam team = await SimpleTeam.FromDto(dto);
                    if (team.IsSavable)
                    {
                        team = await team.SaveAsync();
                    }
                    return Ok(team.ToDto<SimpleTeamDto>());
                });
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
