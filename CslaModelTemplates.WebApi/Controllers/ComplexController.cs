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
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetRootList(
            [FromQuery] RootListCriteria criteria
            )
        {
            try
            {
                RootList list = await RootList.Get(criteria);
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
        [ProducesResponseType(typeof(RootViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRootView(
            [FromQuery] RootViewCriteria criteria
            )
        {
            try
            {
                RootView root = await RootView.Get(criteria);
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
        [ProducesResponseType(typeof(RootDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoot(
            [FromQuery] RootCriteria criteria
            )
        {
            try
            {
                Root root = await Root.Get(criteria);
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
        public async Task<IActionResult> CreateRoot(
            [FromBody] RootDto dto
            )
        {
            try
            {
                Root root = await Root.FromDto(dto);
                if (root.IsValid)
                {
                    root = await root.SaveAsync();
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
        public async Task<IActionResult> UpdateRoot(
            [FromBody] RootDto dto
            )
        {
            try
            {
                Root root = await Root.FromDto(dto);
                if (root.IsSavable)
                {
                    root = await root.SaveAsync();
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
        public async Task<IActionResult> DeleteRoot(
            [FromQuery] RootCriteria criteria
            )
        {
            try
            {
                await Task.Run(() => Root.Delete(criteria));
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Count

        /// <summary>
        /// Counts the roots grouped by the number of their items.
        /// </summary>
        /// <param name="criteria">The criteria of the count roots by item count command.</param>
        /// <returns>The list of the root counts.</returns>
        [HttpPatch("")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> CountRootsCommand(
            [FromBody] CountRootsCriteria criteria
            )
        {
            try
            {
                CountRootsList list = await CountRoots.Execute(criteria);
                return Ok(list);
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
        public async Task<IActionResult> GetRootSet(
            [FromQuery] RootSetCriteria criteria
            )
        {
            try
            {
                RootSet set = await RootSet.Get(criteria);
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
        /// Updates the specified root set.
        /// </summary>
        /// <param name="criteria">The criteria of the root set.</param>
        /// <param name="dto">The data transer objects of the root set.</param>
        /// <returns>The updated root set.</returns>
        [HttpPut("set")]
        [ProducesResponseType(typeof(List<RootSetItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRootSet(
            [FromQuery] RootSetCriteria criteria,
            [FromBody] List<RootSetItemDto> dto
            )
        {
            try
            {
                RootSet set = await RootSet.FromDto(criteria, dto);
                if (set.IsSavable)
                {
                    set = await set.SaveAsync();
                }
                return Ok(set.ToDto<RootSetItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
