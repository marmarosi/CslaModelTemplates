using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.ComplexSet;
using CslaModelTemplates.Models.ComplexSet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.ComplexEndpoints
{
    /// <summary>
    /// Gets the specified team set to edit.
    /// </summary>
    [Route(Routes.Complex)]
    public class ReadSet : BaseAsyncEndpoint
        .WithRequest<TeamSetCriteria>
        .WithResponse<IList<TeamSetItemDto>>
    {
        internal ILogger Logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public ReadSet(
            ILogger<ReadSet> logger
            )
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the specified team set to edit.
        /// </summary>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The requested team set.</returns>
        [HttpGet("set")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the specified team set to edit.",
            Description = "Gets the specified team set to edit.<br>" +
                "Criteria:<br>{<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;teamName: string<br>" +
                "}<br>" +
                "Result: TeamSetItemDto[]",
            OperationId = "TeamSet.Read",
            Tags = new[] { "Complex Endpoints" })
        ]
        public override async Task<ActionResult<IList<TeamSetItemDto>>> HandleAsync(
            [FromQuery] TeamSetCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                TeamSet set = await TeamSet.Get(criteria);
                return Ok(set.ToDto());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, Logger, ex);
            }
        }
    }
}
