using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.PaginatedSortedList;
using CslaModelTemplates.Dal.Contracts;
using CslaModelTemplates.Models.PaginatedSortedList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
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
        .WithResponse<IPaginatedList<PaginatedSortedTeamListItemDto>>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public PaginatedSortedList(
            ILogger<PaginatedSortedList> logger
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
            Description = "Gets the specified page of sorted teams.<br>" +
                "Criteria:<br>{<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;teamName: string,<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;sortBy: string,<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;sortDirection: ascending | descending,<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;pageIndex: number,<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;pageSize: number<br>" +
                "}<br>" +
                "Result: PaginatedSortedTeamListItemDto[]",
            OperationId = "PaginatedSortedTeam.List",
            Tags = new[] { "Pagination Endpoints" })
        ]
        public override async Task<ActionResult<IPaginatedList<PaginatedSortedTeamListItemDto>>> HandleAsync(
            [FromQuery] PaginatedSortedTeamListCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                PaginatedSortedTeamList list = await PaginatedSortedTeamList.Get(criteria);
                return Ok(list.ToPaginatedDto<PaginatedSortedTeamListItemDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
