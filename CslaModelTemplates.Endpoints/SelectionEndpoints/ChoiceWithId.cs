using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.SelectionWithId;
using CslaModelTemplates.Dal.Contracts;
using CslaModelTemplates.Models.SelectionWithId;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.SelectionEndpoints
{
    /// <summary>
    /// Gets the ID-name choice of the teams.
    /// </summary>
    [Route(Routes.Selection)]
    public class ChoiceWithId : BaseAsyncEndpoint
        .WithRequest<TeamIdChoiceCriteria>
        .WithResponse<IList<IdNameOptionDto>>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public ChoiceWithId(
            ILogger<ChoiceWithId> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the ID-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The ID-name choice of the teams.</returns>
        [HttpGet("with-id")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the ID-name choice of the teams.",
            Description = "Gets the ID-name choice of the teams. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamName: string" +
                "<br>}<br>" +
                "Result: IdNameOptionDto[]",
            OperationId = "TeamIdChoice.List",
            Tags = new[] { "Selection Endpoints" })
        ]
        public override async Task<ActionResult<IList<IdNameOptionDto>>> HandleAsync(
            [FromQuery] TeamIdChoiceCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                TeamIdChoice choice = await TeamIdChoice.Get(criteria);
                return Ok(choice.ToDto<IdNameOptionDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
