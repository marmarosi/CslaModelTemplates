using CslaModelTemplates.Contracts.Tree;
using CslaModelTemplates.Models.Tree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CslaModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Defines the API endpoints for trees.
    /// </summary>
    [ApiController]
    [Route("api/tree")]
    [Produces("application/json")]
    public class TreeController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public TreeController(
            ILogger<TreeController> logger
            ) : base(logger)
        { }

        #endregion

        #region Tree

        /// <summary>
        /// Gets the specified folder tree.
        /// </summary>
        /// <param name="criteria">The criteria of the folder tree.</param>
        /// <returns>The requested folder tree.</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(FolderNodeDto), StatusCodes.Status200OK)]
        public IActionResult GetFolderTree(
            [FromQuery] FolderTreeCriteria criteria
            )
        {
            try
            {
                FolderTree tree = FolderTree.Get(criteria);
                return Ok(tree);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
