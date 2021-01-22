using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Contracts.SimpleList;
using CslaModelTemplates.Contracts.SimpleView;
using CslaModelTemplates.Models.Simple;
using CslaModelTemplates.Models.SimpleList;
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
                return Ok(root.AsDto());
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
                return Created(Request.Path, root.AsDto());
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
                return Ok(root.AsDto());
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
    }
}
