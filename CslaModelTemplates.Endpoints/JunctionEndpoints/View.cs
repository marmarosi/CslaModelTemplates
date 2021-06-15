using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.JunctionView;
using CslaModelTemplates.Models.JunctionView;
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
    /// Gets the specified group details to display.
    /// </summary>
    [Route(Routes.Junction)]
    public class View : BaseAsyncEndpoint
        .WithRequest<GroupViewCriteria>
        .WithResponse<GroupViewDto>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public View(
            ILogger<View> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the specified group details to display.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The requested group view.</returns>
        [HttpGet("view")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the specified group details to display.",
            Description = "Gets the specified group details to display. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;GroupKey: number" +
                "<br>}<br>" +
                "Result: GroupViewDto",
            OperationId = "Group.View",
            Tags = new[] { "Junction Endpoints" })
        ]
        public override async Task<ActionResult<GroupViewDto>> HandleAsync(
            [FromQuery] GroupViewCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                GroupView group = await GroupView.Get(criteria);
                return Ok(group.ToDto<GroupViewDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
