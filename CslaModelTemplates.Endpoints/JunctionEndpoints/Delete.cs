using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.Junction;
using CslaModelTemplates.Models.Junction;
using Microsoft.AspNetCore.Http;
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
    /// Deletes the specified group.
    /// </summary>
    [Route(Routes.Junction)]
    public class Delete : BaseAsyncEndpoint
        .WithRequest<GroupParams>
        .WithoutResponse
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Delete(
            ILogger<Delete> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Deletes the specified group.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerOperation(
            Summary = "Deletes the specified group.",
            Description = "Deletes the specified group.<br>" +
                "Criteria:<br>{<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;groupId: string<br>" +
                "}",
            OperationId = "Group.Delete",
            Tags = new[] { "Junction Endpoints" })
        ]
        public override async Task<ActionResult> HandleAsync(
            [FromQuery] GroupParams criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                return await Run.RetryOnDeadlock(async () =>
                {
                    await Task.Run(() => Group.Delete(criteria));
                    return NoContent();
                });
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
