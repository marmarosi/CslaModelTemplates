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
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public SortedList(
            ILogger<SortedList> logger
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
            Description = "Gets the specified teams sorted.<br>" +
                "Criteria:<br>{<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;teamName: string,<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;sortBy: string,<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;sortDirection: ascending | descending<br>" +
                "}<br>" +
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
