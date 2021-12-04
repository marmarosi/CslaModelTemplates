using CslaModelTemplates.Contracts.Complex;
using CslaModelTemplates.Contracts.ComplexCommand;
using CslaModelTemplates.Contracts.ComplexList;
using CslaModelTemplates.Contracts.ComplexSet;
using CslaModelTemplates.Contracts.ComplexView;
using CslaModelTemplates.Models.Complex;
using CslaModelTemplates.Models.ComplexCommand;
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
    /// Contains the API endpoints for complex models.
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
        /// Gets a list of teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>A list of teams.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TeamListItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TeamListItemDto>>> GetTeamList(
            [FromQuery] TeamListCriteria criteria
            )
        {
            try
            {
                TeamList list = await TeamList.Get(criteria);
                return Ok(list.ToDto<TeamListItemDto>());
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
        [ProducesResponseType(typeof(TeamViewDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<TeamViewDto>> GetTeamView(
            [FromQuery] TeamViewParams criteria
            )
        {
            try
            {
                TeamView team = await TeamView.Get(criteria);
                return Ok(team.ToDto<TeamViewDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region New

        /// <summary>
        /// Gets a new team to edit.
        /// </summary>
        /// <returns>The new team.</returns>
        [HttpGet("new")]
        [ProducesResponseType(typeof(TeamDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<TeamDto>> GetNewTeam()
        {
            try
            {
                Team team = await Team.Create();
                return Ok(team.ToDto());
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
        [ProducesResponseType(typeof(TeamDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<TeamDto>> GetTeam(
            [FromQuery] TeamParams criteria
            )
        {
            try
            {
                Team team = await Team.Get(criteria);
                return Ok(team.ToDto());
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
        [HttpPost]
        [ProducesResponseType(typeof(TeamDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<TeamDto>> CreateTeam(
            [FromBody] TeamDto dto
            )
        {
            try
            {
                return await Call<TeamDto>.RetryOnDeadlock(async () =>
                {
                    Team team = await Team.FromDto(dto);
                    if (team.IsValid)
                    {
                        team = await team.SaveAsync();
                    }
                    return Created(Uri, team.ToDto());
                });
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
        [HttpPut]
        [ProducesResponseType(typeof(TeamDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<TeamDto>> UpdateTeam(
            [FromBody] TeamDto dto
            )
        {
            try
            {
                return await Call<TeamDto>.RetryOnDeadlock(async () =>
                {
                    Team team = await Team.FromDto(dto);
                    if (team.IsSavable)
                    {
                        team = await team.SaveAsync();
                    }
                    return Ok(team.ToDto());
                });
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
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteTeam(
            [FromQuery] TeamParams criteria
            )
        {
            try
            {
                return await Run.RetryOnDeadlock(async () =>
                {
                    await Task.Run(() => Team.Delete(criteria));
                    return NoContent();
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Count

        /// <summary>
        /// Counts the teams grouped by the number of their items.
        /// </summary>
        /// <param name="criteria">The criteria of the count teams by item count command.</param>
        /// <returns>The list of the team counts.</returns>
        [HttpPatch]
        [ProducesResponseType(typeof(List<CountTeamsListItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CountTeamsListItemDto>>> CountTeamsCommand(
            [FromBody] CountTeamsCriteria criteria
            )
        {
            try
            {
                CountTeamsList list = await CountTeams.Execute(criteria);
                return Ok(list.ToDto<CountTeamsListItemDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Read-Set

        /// <summary>
        /// Gets the specified team set to edit.
        /// </summary>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <returns>The requested team set.</returns>
        [HttpGet("set")]
        [ProducesResponseType(typeof(List<TeamSetItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TeamSetItemDto>>> GetTeamSet(
            [FromQuery] TeamSetCriteria criteria
            )
        {
            try
            {
                TeamSet set = await TeamSet.Get(criteria);
                return Ok(set.ToDto());
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
        [ProducesResponseType(typeof(List<TeamSetItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TeamSetItemDto>>> UpdateTeamSet(
            [FromQuery] TeamSetCriteria criteria,
            [FromBody] List<TeamSetItemDto> dto
            )
        {
            try
            {
                return await Run.RetryOnDeadlock(async () =>
                {
                    TeamSet set = await TeamSet.FromDto(criteria, dto);
                    if (set.IsSavable)
                    {
                        set = await set.SaveAsync();
                    }
                    return Ok(set.ToDto());
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
