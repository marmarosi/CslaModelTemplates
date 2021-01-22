using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Contracts.SimpleList;
using CslaModelTemplates.Contracts.SimpleView;
using CslaModelTemplates.Models.Simple;
using CslaModelTemplates.Models.SimpleList;
using CslaModelTemplates.Models.SimpleView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CslaModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Defines the API endpoints for simple models.
    /// </summary>
    [Route("api/simple")]
    [ApiController]
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
        public IActionResult GetRoot(
            [FromQuery] RootCriteria criteria
            )
        {
            try
            {
                Root root = Root.Get(criteria);
                return Ok(root);
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
                return Ok(root);
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
                return Ok(root);
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
        public IActionResult DeleteRoot(
            [FromQuery] RootCriteria criteria
            )
        {
            try
            {
                Root.Delete(criteria);
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
