using Ardalis.ApiEndpoints;
using CslaModelTemplates.Contracts.Tree;
using CslaModelTemplates.Models.Tree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.Endpoints.TreeEndpoints
{
    /// <summary>
    /// Gets the specified folder tree.
    /// </summary>
    [Route(Routes.Tree)]
    public class Tree : BaseAsyncEndpoint
        .WithRequest<FolderTreeParams>
        .WithResponse<FolderNodeDto>
    {
        internal ILogger Logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public Tree(
            ILogger<Tree> logger
            )
        {
            Logger = logger;
        }

        /// <summary>
        /// Gets the specified folder tree.
        /// </summary>
        /// <param name="criteria">The criteria of the folder tree.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The requested folder tree.</returns>
        [HttpGet("")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the specified folder tree.",
            Description = "Gets the specified folder tree.<br>" +
                "Criteria:<br>{<br>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;rootKey: number<br>" +
                "<br>}<br>" +
                "Result: FolderNodeDto",
            OperationId = "FolderTree.View",
            Tags = new[] { "Tree Endpoints" })
        ]
        public override async Task<ActionResult<FolderNodeDto>> HandleAsync(
            [FromQuery] FolderTreeParams criteria,
            CancellationToken cancellationToken
            )
        {
            try
            {
                FolderTree tree = await FolderTree.Get(criteria);
                return Ok(tree.ToDto<FolderNodeDto>());
            }
            catch (Exception ex)
            {
                return Helper.HandleError(this, Logger, ex);
            }
        }
    }
}
