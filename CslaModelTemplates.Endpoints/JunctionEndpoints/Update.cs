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
    /// Updates the specified group.
    /// </summary>
    [Route(Routes.Junction)]
    public class Update : BaseAsyncEndpoint
        .WithRequest<GroupDto>
        .WithResponse<GroupDto>
    {
        internal ILogger logger { get; set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Update(
            ILogger logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Updates the specified group.
        /// </summary>
        /// <param name="dto">The data transer object of the group.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated group.</returns>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Updates the specified group.",
            Description = "Updates the specified group.<br>" +
                "Data: GroupDto<br>" +
                "Result: GroupDto",
            OperationId = "Group.Update",
            Tags = new[] { "Junction Endpoints" })
        ]
        public override async Task<ActionResult<GroupDto>> HandleAsync(
            [FromBody] GroupDto dto,
            CancellationToken cancellationToken
            )
        {
            try
            {
                Group group = await Group.FromDto(dto);
                if (group.IsSavable)
                {
                    group = await group.SaveAsync();
                }
                return Ok(group.ToDto<GroupDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
