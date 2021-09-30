using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.SimpleCommand;
using CslaModelTemplates.CslaExtensions;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SimpleCommand
{
    /// <summary>
    /// Renames the specified team.
    /// </summary>
    [Serializable]
    public class RenameTeam : CommandBase<RenameTeam>
    {
        #region Properties

        private long? TeamKey
        {
            get { return KeyHash.Decode(ID.Team, TeamId); }
            set { TeamId = KeyHash.Encode(ID.Team, value); }
        }

        public static readonly PropertyInfo<string> TeamIdProperty = RegisterProperty<string>(c => c.TeamId);
        public string TeamId
        {
            get { return ReadProperty(TeamIdProperty); }
            set { LoadProperty(TeamIdProperty, value); }
        }

        public static readonly PropertyInfo<string> TeamNameProperty = RegisterProperty<string>(c => c.TeamName);
        public string TeamName
        {
            get { return ReadProperty(TeamNameProperty); }
            private set { LoadProperty(TeamNameProperty, value); }
        }

        public static readonly PropertyInfo<bool> ResultProperty = RegisterProperty<bool>(c => c.Result);
        public bool Result
        {
            get { return ReadProperty(ResultProperty); }
            private set { LoadProperty(ResultProperty, value); }
        }

        #endregion

        #region Business Rules

        private void Validate()
        {
            if (string.IsNullOrEmpty(TeamName))
                throw new CommandException(ValidationText.RenameTeam_TeamName_Required);
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(RenameTeam),
        //        new IsInRole(AuthorizationActions.ExecuteMethod, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private RenameTeam()
        { /* require use of factory methods */ }

        /// <summary>
        /// Renames the specified team.
        /// </summary>
        /// <param name="dto">The data transfer object of the command.</param>
        /// <returns>True when the rename succeeded; otherwise false.</returns>
        public static async Task<bool> Execute(
            RenameTeamDto dto
            )
        {
            RenameTeam command = new RenameTeam();
            command.TeamId = dto.TeamId;
            command.TeamName = dto.TeamName;
            command.Result = false;

            command.Validate();

            command = await Task.Run(() => DataPortal.ExecuteAsync(command));
            return command.Result;
        }

        #endregion

        #region Data Access

        protected override void DataPortal_Execute()
        {
            // Execute the command.
            using (var ctx = DalFactory.GetManager())
            {
                IRenameTeamDal dal = ctx.GetProvider<IRenameTeamDal>();

                RenameTeamDao dao = new RenameTeamDao(TeamKey ?? 0, TeamName);
                dal.Execute(dao);

                // Set new data.
                Result = true;
            }
        }

        #endregion
    }
}
