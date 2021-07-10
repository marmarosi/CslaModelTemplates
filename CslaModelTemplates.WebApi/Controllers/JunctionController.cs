using CslaModelTemplates.Contracts.Junction;
using CslaModelTemplates.Contracts.JunctionView;
using CslaModelTemplates.Models.Junction;
using CslaModelTemplates.Models.JunctionView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for junction models.
    /// </summary>
    [ApiController]
    [Route("api/junction")]
    [Produces("application/json")]
    public class JunctionController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public JunctionController(
            ILogger<JunctionController> logger
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

        #region New

        /// <summary>
        /// Gets a new group to edit.
        /// </summary>
        /// <returns>A new group.</returns>
        [HttpGet("new")]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNewGroup()
        {
            try
            {
                Group group = await Group.Create();
                return Ok(group.ToDto<GroupDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Read

        /// <summary>
        /// Gets the specified group to edit.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        /// <returns>The requested group.</returns>
        [HttpGet("fetch")]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroup(
            [FromQuery] GroupCriteria criteria
            )
        {
            try
            {
                Group group = await Group.Get(criteria);
                return Ok(group.ToDto<GroupDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Create

        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="dto">The data transer object of the group.</param>
        /// <returns>The created group.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateGroup(
            [FromBody] GroupDto dto
            )
        {
            try
            {
                return await RetryOnDeadlock(async () =>
                {
                    Group group = await Group.FromDto(dto);
                    if (group.IsValid)
                    {
                        group = await group.SaveAsync();
                    }
                    return Created(Uri, group.ToDto<GroupDto>());
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
        /// Updates the specified group.
        /// </summary>
        /// <param name="dto">The data transer object of the group.</param>
        /// <returns>The updated group.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateGroup(
            [FromBody] GroupDto dto
            )
        {
            try
            {
                return await RetryOnDeadlock(async () =>
                {
                    Group group = await Group.FromDto(dto);
                    if (group.IsSavable)
                    {
                        group = await group.SaveAsync();
                    }
                    return Ok(group.ToDto<GroupDto>());
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
        /// Deletes the specified group.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteGroup(
            [FromQuery] GroupCriteria criteria
            )
        {
            try
            {
                return await RetryOnDeadlock(async () =>
                {
                    await Task.Run(() => Group.Delete(criteria));
                    return NoContent();
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
