using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Models.Simple;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.SimpleEndpoints
{
    /// <summary>
    /// Gets e new team to edit.
    /// </summary>
    public class New : SimpleAsyncEndpoint<object, SimpleTeamDto>
    {
        /// <summary>
        /// Gets e new team to edit.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A new team..</returns>
        [HttpGet("new")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets e new team to edit.",
            Description = "Gets e new team to edit.<br><br>" +
                "Result: SimpleTeamDto",
            OperationId = "SimpleTeam.New",
            Tags = new[] { "Simple Endpoints" })
        ]
        public override async Task<ActionResult<SimpleTeamDto>> HandleAsync(
            object nothing,
            CancellationToken cancellationToken
            )
        {
            try
            {
                SimpleTeam team = await SimpleTeam.Create();
                return Ok(team.ToDto<SimpleTeamDto>());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
