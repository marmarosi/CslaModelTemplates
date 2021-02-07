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
        public IActionResult GetRootList(
            [FromQuery] SimpleRootListCriteria criteria
            )
        {
            try
            {
                SimpleRootList list = SimpleRootList.Get(criteria);
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
        /// Gets the specified root view.
        /// </summary>
        /// <param name="criteria">The criteria of the root view.</param>
        /// <returns>The requested root view.</returns>
        [HttpGet("view")]
        [ProducesResponseType(typeof(SimpleRootViewDto), StatusCodes.Status200OK)]
        public IActionResult GetRootView(
            [FromQuery] SimpleRootViewCriteria criteria
            )
        {
            try
            {
                SimpleRootView root = SimpleRootView.Get(criteria);
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
        /// Gets the specified root.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        /// <returns>The requested root.</returns>
        [HttpGet("fetch")]
        [ProducesResponseType(typeof(SimpleRootDto), StatusCodes.Status200OK)]
        public IActionResult GetRoot(
            [FromQuery] SimpleRootCriteria criteria
            )
        {
            try
            {
                SimpleRoot root = SimpleRoot.Get(criteria);
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
        public IActionResult CreateRoot(
            [FromBody] SimpleRootDto dto
            )
        {
            try
            {
                SimpleRoot root = SimpleRoot.FromDto(dto);
                if (root.IsValid)
                {
                    root = root.Save();
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
        public IActionResult UpdateRoot(
            [FromBody] SimpleRootDto dto
            )
        {
            try
            {
                SimpleRoot root = SimpleRoot.FromDto(dto);
                if (root.IsSavable)
                {
                    root = root.Save();
                }
                return Ok(root.ToDto<SimpleRootDto>());
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
        public IActionResult RenameRootCommand(
            [FromBody] RenameRootDto dto
            )
        {
            try
            {
                RenameRoot command = RenameRoot.Create(dto);
                command.Execute();

                return Ok(command.Result);
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
        public IActionResult DeleteRoot(
            [FromQuery] SimpleRootCriteria criteria
            )
        {
            try
            {
                SimpleRoot.Delete(criteria);
                return NoContent();
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
        public IActionResult GetRootSet(
            [FromQuery] SimpleRootSetCriteria criteria
            )
        {
            try
            {
                SimpleRootSet set = SimpleRootSet.Get(criteria);
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
        /// Updates the specified roots.
        /// </summary>
        /// <param name="dto">The data transer objects of the root set.</param>
        /// <returns>The updated root set.</returns>
        [HttpPut("set")]
        [ProducesResponseType(typeof(List<SimpleRootSetItemDto>), StatusCodes.Status200OK)]
        public IActionResult UpdateRootSet(
            [FromBody] List<SimpleRootSetItemDto> dto
            )
        {
            try
            {
                SimpleRootSet root = SimpleRootSet.FromDto(dto);
                if (root.IsSavable)
                {
                    root = root.Save();
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
