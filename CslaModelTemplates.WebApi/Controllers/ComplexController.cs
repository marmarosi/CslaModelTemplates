using CslaModelTemplates.Contracts.Complex;
using CslaModelTemplates.Contracts.ComplexCommand;
using CslaModelTemplates.Contracts.ComplexList;
using CslaModelTemplates.Contracts.ComplexSet;
using CslaModelTemplates.Contracts.ComplexView;
using CslaModelTemplates.Models.Command;
using CslaModelTemplates.Models.Complex;
using CslaModelTemplates.Models.ComplexList;
using CslaModelTemplates.Models.ComplexSet;
using CslaModelTemplates.Models.ComplexView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Defines the API endpoints for complex models.
    /// </summary>
    [ApiController]
    [Route("api/complex")]
    [Produces("application/json")]
    public class ComplexController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public ComplexController(
            ILogger<ComplexController> logger
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
        [ProducesResponseType(typeof(List<RootListItemDto>), StatusCodes.Status200OK)]
        public IActionResult GetRootList(
            [FromQuery] RootListCriteria criteria
            )
        {
            try
            {
                RootList list = RootList.Get(criteria);
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
        [ProducesResponseType(typeof(RootViewDto), StatusCodes.Status200OK)]
        public IActionResult GetRootView(
            [FromQuery] RootViewCriteria criteria
            )
        {
            try
            {
                RootView root = RootView.Get(criteria);
                return Ok(root);
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
        /// <param name="criteria">The criteria of the count roots by item count command.</param>
        /// <returns>The list of the root counts.</returns>
        [HttpPatch("")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public IActionResult CountRootsCommand(
            [FromBody] CountRootsCriteria criteria
            )
        {
            try
            {
                CountRoots command = CountRoots.Create(criteria);
                command.Execute();

                return Ok(command.Result);
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
        [ProducesResponseType(typeof(RootDto), StatusCodes.Status200OK)]
        public IActionResult GetRoot(
            [FromQuery] RootCriteria criteria
            )
        {
            try
            {
                Root root = Root.Get(criteria);
                return Ok(root.ToDto<RootDto>());
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
        [ProducesResponseType(typeof(RootDto), StatusCodes.Status201Created)]
        public IActionResult CreateRoot(
            [FromBody] RootDto dto
            )
        {
            try
            {
                Root root = Root.FromDto(dto);
                if (root.IsValid)
                {
                    root = root.Save();
                }
                return Created(Request.Path, root.ToDto<RootDto>());
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
        [ProducesResponseType(typeof(RootDto), StatusCodes.Status200OK)]
        public IActionResult UpdateRoot(
            [FromBody] RootDto dto
            )
        {
            try
            {
                Root root = Root.FromDto(dto);
                if (root.IsSavable)
                {
                    root = root.Save();
                }
                return Ok(root.ToDto<RootDto>());
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
            [FromQuery] RootCriteria criteria
            )
        {
            try
            {
                Root.Delete(criteria);
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
        [ProducesResponseType(typeof(List<RootSetItemDto>), StatusCodes.Status200OK)]
        public IActionResult GetRootSet(
            [FromQuery] RootSetCriteria criteria
            )
        {
            try
            {
                RootSet set = RootSet.Get(criteria);
                return Ok(set.ToDto<RootSetItemDto>());
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
        /// <param name="criteria">The criteria of the root set.</param>
        /// <param name="dto">The data transer objects of the root set.</param>
        /// <returns>The updated root set.</returns>
        [HttpPut("set")]
        [ProducesResponseType(typeof(List<RootSetItemDto>), StatusCodes.Status200OK)]
        public IActionResult UpdateRootSet(
            [FromQuery] RootSetCriteria criteria,
            [FromBody] List<RootSetItemDto> dto
            )
        {
            try
            {
                RootSet root = RootSet.FromDto(criteria, dto);
                if (root.IsSavable)
                {
                    root = root.Save();
                }
                return Ok(root.ToDto<RootSetItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
