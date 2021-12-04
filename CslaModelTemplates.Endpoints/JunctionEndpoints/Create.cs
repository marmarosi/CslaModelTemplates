using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.Junction;
using CslaModelTemplates.Models.Junction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.JunctionEndpoints
{
    /// <summary>
    /// Creates a new group.
    /// </summary>
    [Route(Routes.Junction)]
    public class Create : BaseAsyncEndpoint
        .WithRequest<GroupDto>
        .WithResponse<GroupDto>
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
        /// Creates a new group.
        /// </summary>
        /// <param name="dto">The data transer object of the group.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The created group.</returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Creates a new group.",
            Description = "Creates a new group.<br>" +
                "Data: GroupDto<br>" +
                "Result: GroupDto",
            OperationId = "Group.Create",
            Tags = new[] { "Junction Endpoints" })
        ]
        public override async Task<ActionResult<GroupDto>> HandleAsync(
            [FromBody] GroupDto dto,
            CancellationToken cancellationToken
            )
        {
            try
            {
                return await Call<GroupDto>.RetryOnDeadlock(async () =>
                {
                    Group group = await Group.FromDto(dto);
                    if (group.IsValid)
                    {
                        group = await group.SaveAsync();
                    }
                    return Created(Helper.Uri(Request), group.ToDto());
                });
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, Logger, ex);
            }
        }
    }
}
