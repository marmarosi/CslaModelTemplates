using Ardalis.ApiEndpoints;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionWithKey;
using CslaModelTemplates.Models.SelectionWithKey;
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
    /// Gets the key-name choice of the teams.
    /// </summary>
    [Route(Routes.Selection)]
    public class ChoiceWithKey : BaseAsyncEndpoint
        .WithRequest<TeamKeyChoiceCriteria>
        .WithResponse<IList<KeyNameOptionDto>>
    {
        internal ILogger logger { get; set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public ChoiceWithKey(
            ILogger logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the key-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The key-name choice of the teams.</returns>
        [HttpGet("with-key")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the key-name choice of the teams.",
            Description = "Gets the key-name choice of the teams. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamName: string" +
                "<br>}<br>" +
                "Result: KeyNameOptionDto[]",
            OperationId = "TeamKeyChoice.List",
            Tags = new[] { "Selection Endpoints" })
        ]
        public override async Task<ActionResult<IList<KeyNameOptionDto>>> HandleAsync(
            [FromQuery] TeamKeyChoiceCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                TeamKeyChoice choice = await TeamKeyChoice.Get(criteria);
                return Ok(choice.ToDto<KeyNameOptionDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
