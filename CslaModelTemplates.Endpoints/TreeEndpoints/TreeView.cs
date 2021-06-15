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
    public class TreeView : BaseAsyncEndpoint
        .WithRequest<FolderTreeCriteria>
        .WithResponse<FolderNodeDto>
    {
        internal ILogger Logger { get; private set; }

        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public TreeView(
            ILogger<TreeView> logger
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
        [HttpGet("view")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Gets the specified folder tree.",
            Description = "Gets the specified folder tree. Criteria:<br>{" +
                "<br>&nbsp;&nbsp;&nbsp;&nbsp;RootKey: number" +
                "<br>}<br>" +
                "Result: FolderNodeDto",
            OperationId = "FolderTree.View",
            Tags = new[] { "Tree Endpoints" })
        ]
        public override async Task<ActionResult<FolderNodeDto>> HandleAsync(
            [FromQuery] FolderTreeCriteria criteria,
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
