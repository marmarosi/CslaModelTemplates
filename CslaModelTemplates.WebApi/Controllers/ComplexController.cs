//using CslaModelTemplates.Contracts.Complex;
//using CslaModelTemplates.Contracts.ComplexCommand;
//using CslaModelTemplates.Contracts.ComplexList;
using CslaModelTemplates.Contracts.ComplexView;
using CslaModelTemplates.Models.Complex;
//using CslaModelTemplates.Models.ComplexCommand;
using CslaModelTemplates.Models.ComplexList;
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
    }
}
