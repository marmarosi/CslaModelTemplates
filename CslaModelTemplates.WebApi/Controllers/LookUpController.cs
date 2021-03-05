using CslaModelTemplates.Contracts.LookUpView;
using CslaModelTemplates.Models.LookUpView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for simple models.
    /// </summary>
    [ApiController]
    [Route("api/look-up")]
    [Produces("application/json")]
    public class LookUpController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public LookUpController(
            ILogger<LookUpController> logger
            ) : base(logger)
        { }

        #endregion

        #region View

        /// <summary>
        /// Gets the specified group details to display.
        /// </summary>
        /// <param name="criteria">The criteria of the group view.</param>
        /// <returns>The requested group view.</returns>
        [HttpGet("view")]
        [ProducesResponseType(typeof(GroupViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroupView(
            [FromQuery] GroupViewCriteria criteria
            )
        {
            try
            {
                GroupView group = await GroupView.Get(criteria);
                return Ok(group.ToDto<GroupViewDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
