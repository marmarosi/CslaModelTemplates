using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Models.Simple;
using Microsoft.AspNetCore.Http;
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
    /// Creates a new team.
    /// </summary>
    [Route(Routes.Simple)]
    public class Create : BaseAsyncEndpoint
        .WithRequest<SimpleTeamDto>
        .WithResponse<SimpleTeamDto>
    {
        internal ILogger Logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Create(
            ILogger<Create> logger
            )
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Creates a new team.
        /// </summary>
        /// <param name="dto">The data transer object of the team.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created team.</returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerOperation(
            Summary = "Creates a new team.",
            Description = "Creates a new team.<br>" +
                "Data: SimpleTeamDto<br>" +
                "Result: SimpleTeamDto",
            OperationId = "SimpleTeam.Create",
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
                    if (team.IsValid)
                    {
                        team = await team.SaveAsync();
                    }
                    return Created(Helper.Uri(Request), team.ToDto());
                });
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, Logger, ex);
            }
        }
    }
}
