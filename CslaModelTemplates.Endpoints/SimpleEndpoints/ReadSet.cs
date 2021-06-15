using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Models.SimpleSet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.SimpleEndpoints
{
    /// <summary>
    /// Gets the specified team set to edit.
    /// </summary>
    [Route(Routes.Simple)]
    public class ReadSet : BaseAsyncEndpoint
        .WithRequest<SimpleTeamSetCriteria>
        .WithResponse<IList<SimpleTeamSetItemDto>>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public ReadSet(
            ILogger<ReadSet> logger
            )
        {
            this.logger = logger;
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
            Description = "Gets the specified team set to edit. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamName: string" +
                "<br>}<br>" +
                "Result: SimpleTeamSetItemDto[]",
            OperationId = "SimpleTeamSet.Read",
            Tags = new[] { "Simple Endpoints" })
        ]
        public override async Task<ActionResult<IList<SimpleTeamSetItemDto>>> HandleAsync(
            [FromQuery] SimpleTeamSetCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                SimpleTeamSet set = await SimpleTeamSet.Get(criteria);
                return Ok(set.ToDto<SimpleTeamSetItemDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
