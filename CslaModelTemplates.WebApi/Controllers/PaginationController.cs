using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.PaginatedList;
using CslaModelTemplates.Models.PaginatedList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Defines the API endpoints for pagination.
    /// </summary>
    [ApiController]
    [Route("api/pagination")]
    [Produces("application/json")]
    public class PaginationController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public PaginationController(
            ILogger<TreeController> logger
            ) : base(logger)
        { }

        #endregion

        #region PaginatedTeamList

        /// <summary>
        /// Gets the specified page of teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested page of the team list.</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(PaginatedList<PaginatedTeamListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaginatedTeamList(
            [FromQuery] PaginatedTeamListCriteria criteria
            )
        {
            try
            {
                PaginatedTeamList tree = await PaginatedTeamList.Get(criteria);
                return Ok(tree.ToDto<PaginatedList<PaginatedTeamListItemDto>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
