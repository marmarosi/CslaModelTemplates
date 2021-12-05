using Ardalis.ApiEndpoints;
using CslaModelTemplates.Dal.Contracts;
using CslaModelTemplates.Models.TreeSelection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.TreeEndpoints
{
    /// <summary>
    /// Gets the ID-name choice of the teams.
    /// </summary>
    [Route(Routes.Tree)]
    public class Choice : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<IList<IdNameOptionDto>>
    {
        internal ILogger logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Choice(
            ILogger<Choice> logger
            )
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets the ID-name choice of the trees.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The ID-name choice of the trees.</returns>
        [HttpGet("choice")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the ID-name choice of the trees.",
            Description = "Gets the ID-name choice of the teams.<br>" +
                "Result: IdNameOptionDto[]",
            OperationId = "FolderTree.Choice",
            Tags = new[] { "Tree Endpoints" })
        ]
        public override async Task<ActionResult<IList<IdNameOptionDto>>> HandleAsync(
            CancellationToken cancellationToken
            )
        {
            try
            {
                RootFolderChoice choice = await RootFolderChoice.Get();
                return Ok(choice.ToDto<IdNameOptionDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, logger, ex);
            }
        }
    }
}
