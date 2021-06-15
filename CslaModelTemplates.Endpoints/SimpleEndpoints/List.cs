using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.SimpleList;
using CslaModelTemplates.Models.SimpleList;
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
    /// Gets a list of teams.
    /// </summary>
    [Route(Routes.Simple)]
    public class List : BaseAsyncEndpoint
        .WithRequest<SimpleTeamListCriteria>
        .WithResponse<IList<SimpleTeamListItemDto>>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public List(
            ILogger<List> logger
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
                "Result: SimpleTeamListItemDto[]",
            OperationId = "SimpleTeam.List",
            Tags = new[] { "Simple Endpoints" })
        ]
        public override async Task<ActionResult<IList<SimpleTeamListItemDto>>> HandleAsync(
            [FromQuery] SimpleTeamListCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                SimpleTeamList list = await SimpleTeamList.Get(criteria);
                return Ok(list.ToDto<SimpleTeamListItemDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
