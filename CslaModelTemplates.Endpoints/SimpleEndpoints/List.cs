using CslaModelTemplates.Contracts.SimpleList;
using CslaModelTemplates.Models.SimpleList;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.SimpleEndpoints
{
    /// <summary>
    /// Gets a list of teams.
    /// </summary>
    public class List : SimpleAsyncEndpoint<SimpleTeamListCriteria, IList<SimpleTeamListItemDto>>
    {
        /// <summary>
        /// Gets a list of teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of teams.</returns>
        [HttpGet]
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
                return Problem(ex.Message);
            }
        }
    }
}
