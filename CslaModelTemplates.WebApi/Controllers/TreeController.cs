using CslaModelTemplates.Contracts.Tree;
using CslaModelTemplates.Dal.Contracts;
using CslaModelTemplates.Models.Tree;
using CslaModelTemplates.Models.TreeSelection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for trees.
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

        #region View

        /// <summary>
        /// Gets the specified folder tree.
        /// </summary>
        /// <param name="criteria">The criteria of the folder tree.</param>
        /// <returns>The requested folder tree.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(FolderNodeDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<FolderNodeDto>> GetFolderTree(
            [FromQuery] FolderTreeParams criteria
            )
        {
            try
            {
                FolderTree tree = await FolderTree.Get(criteria);
                return Ok(tree.ToDto<FolderNodeDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Choice

        /// <summary>
        /// Gets the ID-name choice of the trees.
        /// </summary>
        /// <returns>The ID-name choice of the trees.</returns>
        [HttpGet("choice")]
        [ProducesResponseType(typeof(List<IdNameOptionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<IdNameOptionDto>>> GetRootFolderChoice()
        {
            try
            {
                RootFolderChoice choice = await RootFolderChoice.Get();
                return Ok(choice.ToDto<IdNameOptionDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
