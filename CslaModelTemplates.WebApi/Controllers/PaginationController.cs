using CslaModelTemplates.Common.DataTransfer;
using CslaModelTemplates.Contracts.PaginatedList;
using CslaModelTemplates.Contracts.PaginatedSortedList;
using CslaModelTemplates.Contracts.SortedList;
using CslaModelTemplates.Models.PaginatedList;
using CslaModelTemplates.Models.PaginatedSortedList;
using CslaModelTemplates.Models.SortedList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            ILogger<PaginationController> logger
            ) : base(logger)
        { }

        #endregion

        #region SortedTeamList

        /// <summary>
        /// Gets the specified teams sorted.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested team list.</returns>
        [HttpGet("sorted")]
        [ProducesResponseType(typeof(List<SortedTeamListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSortedTeamList(
            [FromQuery] SortedTeamListCriteria criteria
            )
        {
            try
            {
                SortedTeamList list = await SortedTeamList.Get(criteria);
                return Ok(list.ToDto<SortedTeamListItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region PaginatedTeamList

        /// <summary>
        /// Gets the specified page of teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested page of the team list.</returns>
        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PaginatedList<PaginatedTeamListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaginatedTeamList(
            [FromQuery] PaginatedTeamListCriteria criteria
            )
        {
            try
            {
                PaginatedTeamList list = await PaginatedTeamList.Get(criteria);
                return Ok(list.ToDto<PaginatedList<PaginatedTeamListItemDto>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region PaginatedSortedTeamList

        /// <summary>
        /// Gets the specified page of sorted teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested page of the sorted team list.</returns>
        [HttpGet("paginated-sorted")]
        [ProducesResponseType(typeof(PaginatedList<PaginatedSortedTeamListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaginatedSortedTeamList(
            [FromQuery] PaginatedSortedTeamListCriteria criteria
            )
        {
            try
            {
                PaginatedSortedTeamList list = await PaginatedSortedTeamList.Get(criteria);
                return Ok(list.ToDto<PaginatedList<PaginatedSortedTeamListItemDto>>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
