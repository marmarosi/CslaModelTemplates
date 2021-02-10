using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Contracts.SimpleCommand;
using CslaModelTemplates.Contracts.SimpleList;
using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Contracts.SimpleView;
using CslaModelTemplates.Models.Simple;
using CslaModelTemplates.Models.SimpleCommand;
using CslaModelTemplates.Models.SimpleList;
using CslaModelTemplates.Models.SimpleSet;
using CslaModelTemplates.Models.SimpleView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Defines the API endpoints for simple models.
    /// </summary>
    [ApiController]
    [Route("api/simple")]
    [Produces("application/json")]
    public class SimpleController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public SimpleController(
            ILogger<SimpleController> logger
            ) : base(logger)
        { }

        #endregion

        #region List

        /// <summary>
        /// Gets a list of roots.
        /// </summary>
        /// <param name="criteria">The criteria of the root list.</param>
        /// <returns>A list of roots.</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<SimpleRootListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRootList(
            [FromQuery] SimpleRootListCriteria criteria
            )
        {
            try
            {
                SimpleRootList list = await SimpleRootList.Get(criteria);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region View

        /// <summary>
        /// Gets the specified root details to display.
        /// </summary>
        /// <param name="criteria">The criteria of the root view.</param>
        /// <returns>The requested root view.</returns>
        [HttpGet("view")]
        [ProducesResponseType(typeof(SimpleRootViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRootView(
            [FromQuery] SimpleRootViewCriteria criteria
            )
        {
            try
            {
                SimpleRootView root = await SimpleRootView.Get(criteria);
                return Ok(root);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Read

        /// <summary>
        /// Gets the specified root to edit.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        /// <returns>The requested root.</returns>
        [HttpGet("fetch")]
        [ProducesResponseType(typeof(SimpleRootDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoot(
            [FromQuery] SimpleRootCriteria criteria
            )
        {
            try
            {
                SimpleRoot root = await SimpleRoot.Get(criteria);
                return Ok(root.ToDto<SimpleRootDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Create

        /// <summary>
        /// Creates a new root.
        /// </summary>
        /// <param name="dto">The data transer object of the root.</param>
        /// <returns>The created root.</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(SimpleRootDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRoot(
            [FromBody] SimpleRootDto dto
            )
        {
            try
            {
                SimpleRoot root = await SimpleRoot.FromDto(dto);
                if (root.IsValid)
                {
                    root = await root.SaveAsync();
                }
                return Created(Request.Path, root.ToDto<SimpleRootDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the specified root.
        /// </summary>
        /// <param name="dto">The data transer object of the root.</param>
        /// <returns>The updated root.</returns>
        [HttpPut("")]
        [ProducesResponseType(typeof(SimpleRootDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRoot(
            [FromBody] SimpleRootDto dto
            )
        {
            try
            {
                SimpleRoot root = await SimpleRoot.FromDto(dto);
                if (root.IsSavable)
                {
                    root = await root.SaveAsync();
                }
                return Ok(root.ToDto<SimpleRootDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified root.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        [HttpDelete("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRoot(
            [FromQuery] SimpleRootCriteria criteria
            )
        {
            try
            {
                await Task.Run(() => SimpleRoot.Delete(criteria));
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Rename

        /// <summary>
        /// Renames the specified root.
        /// </summary>
        /// <param name="dto">The data transer object of the rename root command.</param>
        /// <returns>True when the root was renamed; otherwise false.</returns>
        [HttpPatch("")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RenameRootCommand(
            [FromBody] RenameRootDto dto
            )
        {
            try
            {
                bool result = await RenameRoot.Execute(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Read-Set

        /// <summary>
        /// Gets the specified root set.
        /// </summary>
        /// <param name="criteria">The criteria of the root set.</param>
        /// <returns>The requested root set.</returns>
        [HttpGet("set")]
        [ProducesResponseType(typeof(List<SimpleRootSetItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRootSet(
            [FromQuery] SimpleRootSetCriteria criteria
            )
        {
            try
            {
                SimpleRootSet set = await SimpleRootSet.Get(criteria);
                return Ok(set.ToDto<SimpleRootSetItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Update-Set

        /// <summary>
        /// Updates the specified root set.
        /// </summary>
        /// <param name="criteria">The criteria of the root set.</param>
        /// <param name="dto">The data transer objects of the root set.</param>
        /// <returns>The updated root set.</returns>
        [HttpPut("set")]
        [ProducesResponseType(typeof(List<SimpleRootSetItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRootSet(
            [FromQuery] SimpleRootSetCriteria criteria,
            [FromBody] List<SimpleRootSetItemDto> dto
            )
        {
            try
            {
                SimpleRootSet root = await SimpleRootSet.FromDto(criteria, dto);
                if (root.IsSavable)
                {
                    root = await root.SaveAsync();
                }
                return Ok(root.ToDto<SimpleRootSetItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
