using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.PaginatedSortedList;
using CslaModelTemplates.Models.PaginatedSortedList;
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
    /// Gets the specified page of sorted teams.
    /// </summary>
    [Route(Routes.Pagination)]
    public class PaginatedSortedList : BaseAsyncEndpoint
        .WithRequest<PaginatedSortedTeamListCriteria>
        .WithResponse<IList<PaginatedSortedTeamListItemDto>>
    {
        internal ILogger logger { get; set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public PaginatedSortedList(
            ILogger logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the specified page of sorted teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The requested page of the sorted team list.</returns>
        [HttpGet("paginated-sorted")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the specified page of sorted teams.",
            Description = "Gets the specified page of sorted teams. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamName: string" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;SortBy: string" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;SortDirection: ascending | descending" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;PageIndex: number" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;PageSize: number" +
                "<br>}<br>" +
                "Result: PaginatedSortedTeamListItemDto[]",
            OperationId = "PaginatedSortedTeam.List",
            Tags = new[] { "Pagination Endpoints" })
        ]
        public override async Task<ActionResult<IList<PaginatedSortedTeamListItemDto>>> HandleAsync(
            [FromQuery] PaginatedSortedTeamListCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                PaginatedSortedTeamList list = await PaginatedSortedTeamList.Get(criteria);
                return Ok(list.ToDto<PaginatedSortedTeamListItemDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
