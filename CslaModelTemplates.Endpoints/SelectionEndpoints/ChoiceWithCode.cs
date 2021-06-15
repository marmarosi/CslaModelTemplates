using Ardalis.ApiEndpoints;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SelectionWithCode;
using CslaModelTemplates.Models.SelectionWithCode;
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
    /// Gets the code-name choice of the teams.
    /// </summary>
    [Route(Routes.Selection)]
    public class ChoiceWithCode : BaseAsyncEndpoint
        .WithRequest<TeamCodeChoiceCriteria>
        .WithResponse<IList<CodeNameOptionDto>>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public ChoiceWithCode(
            ILogger<ChoiceWithCode> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the code-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The key-name choice of the teams.</returns>
        [HttpGet("with-code")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the code-name choice of the teams.",
            Description = "Gets the code-name choice of the teams. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;TeamName: string" +
                "<br>}<br>" +
                "Result: CodeNameOptionDto[]",
            OperationId = "ChoiceWithCode.List",
            Tags = new[] { "Selection Endpoints" })
        ]
        public override async Task<ActionResult<IList<CodeNameOptionDto>>> HandleAsync(
            [FromQuery] TeamCodeChoiceCriteria criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                TeamCodeChoice choice = await TeamCodeChoice.Get(criteria);
                return Ok(choice.ToDto<CodeNameOptionDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
