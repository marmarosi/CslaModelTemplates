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
    /// Gets the specified group to edit.
    /// </summary>
    [Route(Routes.Junction)]
    public class Read : BaseAsyncEndpoint
        .WithRequest<GroupParams>
        .WithResponse<GroupDto>
    {
        internal ILogger Logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Read(
            ILogger<Read> logger
            )
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the specified group to edit.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The requested group.</returns>
        [HttpGet("read")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the specified group to edit.",
            Description = "Gets the specified group to edit.<br>" +
                "Criteria:<br>{<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;groupId: string<br>" +
                "}<br>" +
                "Result: GroupDto",
            OperationId = "Group.Read",
            Tags = new[] { "Junction Endpoints" })
        ]
        public override async Task<ActionResult<GroupDto>> HandleAsync(
            [FromQuery] GroupParams criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                Group group = await Group.Get(criteria);
                return Ok(group.ToDto());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, Logger, ex);
            }
        }
    }
}
