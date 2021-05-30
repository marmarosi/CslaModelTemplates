using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.ComplexList;
using CslaModelTemplates.Models.ComplexList;
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
    /// Gets a list of teams.
    /// </summary>
    [Route(Routes.Complex)]
    public class List : BaseAsyncEndpoint
        .WithRequest<TeamListCriteria>
        .WithResponse<IList<TeamListItemDto>>
    {
        internal ILogger logger { get; set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public List(
            ILogger logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets a list of teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of teams.</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets a list of teams.",
            Description = "Gets a list of teams. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamName: string" +
                "<br>}<br>" +
                "Result: TeamListItemDto[]",
            OperationId = "Team.List",
            Tags = new[] { "Complex Endpoints" })
        ]
        public override async Task<ActionResult<IList<TeamListItemDto>>> HandleAsync(
            [FromQuery] TeamListCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                TeamList list = await TeamList.Get(criteria);
                return Ok(list.ToDto<TeamListItemDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
