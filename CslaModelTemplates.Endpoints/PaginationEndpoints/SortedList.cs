using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.SortedList;
using CslaModelTemplates.Models.SortedList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.PaginationEndpoints
{
    /// <summary>
    /// Gets the specified teams sorted.
    /// </summary>
    [Route(Routes.Pagination)]
    public class SortedList : BaseAsyncEndpoint
        .WithRequest<SortedTeamListCriteria>
        .WithResponse<IList<SortedTeamListItemDto>>
    {
        internal ILogger logger { get; set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public SortedList(
            ILogger logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the specified teams sorted.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The requested team list.</returns>
        [HttpGet("sorted")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the specified teams sorted.",
            Description = "Gets the specified teams sorted. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamName: string" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;SortBy: string" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;SortDirection: ascending | descending" +
                "<br>}<br>" +
                "Result: SortedTeamListItemDto[]",
            OperationId = "SortedTeam.List",
            Tags = new[] { "Pagination Endpoints" })
        ]
        public override async Task<ActionResult<IList<SortedTeamListItemDto>>> HandleAsync(
            [FromQuery] SortedTeamListCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                SortedTeamList list = await SortedTeamList.Get(criteria);
                return Ok(list.ToDto<SortedTeamListItemDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
