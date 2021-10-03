using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.PaginatedList;
using CslaModelTemplates.Dal.Contracts;
using CslaModelTemplates.Models.PaginatedList;
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
    /// Gets the specified page of teams.
    /// </summary>
    [Route(Routes.Pagination)]
    public class PaginatedList : BaseAsyncEndpoint
        .WithRequest<PaginatedTeamListCriteria>
        .WithResponse<IPaginatedList<PaginatedTeamListItemDto>>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public PaginatedList(
            ILogger<PaginatedList> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the specified page of teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The requested page of the team list.</returns>
        [HttpGet("paginated")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the specified page of teams.",
            Description = "Gets the specified page of teams. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamName: string" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;PageIndex: number" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;PageSize: number" +
                "<br>}<br>" +
                "Result: PaginatedTeamListItemDto[]",
            OperationId = "PaginatedTeam.List",
            Tags = new[] { "Pagination Endpoints" })
        ]
        public override async Task<ActionResult<IPaginatedList<PaginatedTeamListItemDto>>> HandleAsync(
            [FromQuery] PaginatedTeamListCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                PaginatedTeamList list = await PaginatedTeamList.Get(criteria);
                return Ok(list.ToPaginatedDto<PaginatedTeamListItemDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
