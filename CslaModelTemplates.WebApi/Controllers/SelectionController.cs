using CslaModelTemplates.Dal.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeDef = CslaModelTemplates.Contracts.SelectionWithCode;
using CodeModel = CslaModelTemplates.Models.SelectionWithCode;
using IdDef = CslaModelTemplates.Contracts.SelectionWithId;
using IdModel = CslaModelTemplates.Models.SelectionWithId;
using KeyDef = CslaModelTemplates.Contracts.SelectionWithKey;
using KeyModel = CslaModelTemplates.Models.SelectionWithKey;

namespace CslaModelTemplates.WebApi.Controllers
{
    /// <summary>
    /// Contains the API endpoints for selections.
    /// </summary>
    [ApiController]
    [Route("api/selection")]
    [Produces("application/json")]
    public class SelectionController : ApiController
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        public SelectionController(
            ILogger<SelectionController> logger
            ) : base(logger)
        { }

        #endregion

        #region Choice with key

        /// <summary>
        /// Gets the key-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The key-name choice of the teams.</returns>
        [HttpGet("with-key")]
        [ProducesResponseType(typeof(List<KeyNameOptionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<KeyNameOptionDto>>> GetTeamChoiceWithKey(
            [FromQuery] KeyDef.TeamKeyChoiceCriteria criteria
            )
        {
            try
            {
                KeyModel.TeamKeyChoice choice = await KeyModel.TeamKeyChoice.Get(criteria);
                return Ok(choice.ToDto<KeyNameOptionDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Choice with ID

        /// <summary>
        /// Gets the ID-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The ID-name choice of the teams.</returns>
        [HttpGet("with-id")]
        [ProducesResponseType(typeof(List<IdNameOptionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<IdNameOptionDto>>> GetTeamChoiceWithId(
            [FromQuery] IdDef.TeamIdChoiceCriteria criteria
            )
        {
            try
            {
                IdModel.TeamIdChoice choice = await IdModel.TeamIdChoice.Get(criteria);
                return Ok(choice.ToDto<IdNameOptionDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion

        #region Choice with code

        /// <summary>
        /// Gets the code-name choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The code-name choice of the tenants.</returns>
        [HttpGet("with-code")]
        [ProducesResponseType(typeof(List<CodeNameOptionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CodeNameOptionDto>>> GetTeamChoiceWithCode(
            [FromQuery] CodeDef.TeamCodeChoiceCriteria criteria
            )
        {
            try
            {
                CodeModel.TeamCodeChoice choice = await CodeModel.TeamCodeChoice.Get(criteria);
                return Ok(choice.ToDto<CodeNameOptionDto>());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        #endregion
    }
}
