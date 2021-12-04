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
    /// Updates the specified team.
    /// </summary>
    [Route(Routes.Complex)]
    public class Update : BaseAsyncEndpoint
        .WithRequest<TeamDto>
        .WithResponse<TeamDto>
    {
        internal ILogger Logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Update(
            ILogger<Update> logger
            )
        {
            this.Logger = logger;
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
                "Data: TeamDto<br>" +
                "Result: TeamDto",
            OperationId = "Team.Update",
            Tags = new[] { "Complex Endpoints" })
        ]
        public override async Task<ActionResult<TeamDto>> HandleAsync(
            [FromBody] TeamDto dto,
            CancellationToken cancellationToken
            )
        {
            try
            {
                return await Call<TeamDto>.RetryOnDeadlock(async () =>
                {
                    Team team = await Team.FromDto(dto);
                    if (team.IsSavable)
                    {
                        team = await team.SaveAsync();
                    }
                    return Ok(team.ToDto());
                });
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, Logger, ex);
            }
        }
    }
}
