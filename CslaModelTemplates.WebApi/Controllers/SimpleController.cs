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
        /// Gets a list of teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>A list of teams.</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<SimpleTeamListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamList(
            [FromQuery] SimpleTeamListCriteria criteria
            )
        {
            try
            {
                SimpleTeamList list = await SimpleTeamList.Get(criteria);
                return Ok(list.ToDto<SimpleTeamListItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region View

        /// <summary>
        /// Gets the specified team details to display.
        /// </summary>
        /// <param name="criteria">The criteria of the team view.</param>
        /// <returns>The requested team view.</returns>
        [HttpGet("view")]
        [ProducesResponseType(typeof(SimpleTeamViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamView(
            [FromQuery] SimpleTeamViewCriteria criteria
            )
        {
            try
            {
                SimpleTeamView team = await SimpleTeamView.Get(criteria);
                return Ok(team.ToDto<SimpleTeamViewDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region New

        /// <summary>
        /// Gets e new team to edit.
        /// </summary>
        /// <returns>The new team.</returns>
        [HttpGet("new")]
        [ProducesResponseType(typeof(SimpleTeamDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNewTeam()
        {
            try
            {
                SimpleTeam team = await SimpleTeam.Create();
                return Ok(team.ToDto<SimpleTeamDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Read

        /// <summary>
        /// Gets the specified team to edit.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        /// <returns>The requested team.</returns>
        [HttpGet("fetch")]
        [ProducesResponseType(typeof(SimpleTeamDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeam(
            [FromQuery] SimpleTeamCriteria criteria
            )
        {
            try
            {
                SimpleTeam team = await SimpleTeam.Get(criteria);
                return Ok(team.ToDto<SimpleTeamDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Create

        /// <summary>
        /// Creates a new team.
        /// </summary>
        /// <param name="dto">The data transer object of the team.</param>
        /// <returns>The created team.</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(SimpleTeamDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTeam(
            [FromBody] SimpleTeamDto dto
            )
        {
            try
            {
                SimpleTeam team = await SimpleTeam.FromDto(dto);
                if (team.IsValid)
                {
                    team = await team.SaveAsync();
                }
                return Created(Uri, team.ToDto<SimpleTeamDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the specified team.
        /// </summary>
        /// <param name="dto">The data transer object of the team.</param>
        /// <returns>The updated team.</returns>
        [HttpPut("")]
        [ProducesResponseType(typeof(SimpleTeamDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTeam(
            [FromBody] SimpleTeamDto dto
            )
        {
            try
            {
                SimpleTeam team = await SimpleTeam.FromDto(dto);
                if (team.IsSavable)
                {
                    team = await team.SaveAsync();
                }
                return Ok(team.ToDto<SimpleTeamDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified team.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        [HttpDelete("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTeam(
            [FromQuery] SimpleTeamCriteria criteria
            )
        {
            try
            {
                await Task.Run(() => SimpleTeam.Delete(criteria));
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
        /// Renames the specified team.
        /// </summary>
        /// <param name="dto">The data transer object of the rename team command.</param>
        /// <returns>True when the team was renamed; otherwise false.</returns>
        [HttpPatch("")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RenameTeamCommand(
            [FromBody] RenameTeamDto dto
            )
        {
            try
            {
                bool result = await RenameTeam.Execute(dto);
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
        /// Gets the specified team set.
        /// </summary>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <returns>The requested team set.</returns>
        [HttpGet("set")]
        [ProducesResponseType(typeof(List<SimpleTeamSetItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamSet(
            [FromQuery] SimpleTeamSetCriteria criteria
            )
        {
            try
            {
                SimpleTeamSet set = await SimpleTeamSet.Get(criteria);
                return Ok(set.ToDto<SimpleTeamSetItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Update-Set

        /// <summary>
        /// Updates the specified team set.
        /// </summary>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <param name="dto">The data transer objects of the team set.</param>
        /// <returns>The updated team set.</returns>
        [HttpPut("set")]
        [ProducesResponseType(typeof(List<SimpleTeamSetItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTeamSet(
            [FromQuery] SimpleTeamSetCriteria criteria,
            [FromBody] List<SimpleTeamSetItemDto> dto
            )
        {
            try
            {
                SimpleTeamSet team = await SimpleTeamSet.FromDto(criteria, dto);
                if (team.IsSavable)
                {
                    team = await team.SaveAsync();
                }
                return Ok(team.ToDto<SimpleTeamSetItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
