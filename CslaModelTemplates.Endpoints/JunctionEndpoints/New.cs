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
    /// Gets a new group to edit.
    /// </summary>
    [Route(Routes.Junction)]
    public class New : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<GroupDto>
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
        /// Gets a new group to edit.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A new group.</returns>
        [HttpGet("new")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets a new group to edit.",
            Description = "Gets a new group to edit.<br><br>" +
                "Result: GroupDto",
            OperationId = "Group.New",
            Tags = new[] { "Junction Endpoints" })
        ]
        public override async Task<ActionResult<GroupDto>> HandleAsync(
            CancellationToken cancellationToken
            )
        {
            try
            {
                Group group = await Group.Create();
                return Ok(group.ToDto<GroupDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
