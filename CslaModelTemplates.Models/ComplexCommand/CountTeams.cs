using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts.ComplexCommand;
using CslaModelTemplates.CslaExtensions;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.ComplexCommand
{
    /// <summary>
    /// Counts the roots grouped by the number of their items.
    /// </summary>
    [Serializable]
    public class CountTeams : CommandBase<CountTeams>
    {
        #region Properties

        public static readonly PropertyInfo<string> TeamNameProperty = RegisterProperty<string>(c => c.TeamName);
        public string TeamName
        {
            get { return ReadProperty(TeamNameProperty); }
            private set { LoadProperty(TeamNameProperty, value); }
        }

        public static readonly PropertyInfo<CountTeamsList> ResultProperty = RegisterProperty<CountTeamsList>(c => c.Result);
        public CountTeamsList Result
        {
            get { return ReadProperty(ResultProperty); }
            private set { LoadProperty(ResultProperty, value); }
        }

        #endregion

        #region Business Rules

        //private void Validate()
        //{
        //    if (string.IsNullOrEmpty(TeamName))
        //        throw new CommandException(ValidationText.CountTeams_TeamName_Required);
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountTeams),
        //        new IsInRole(AuthorizationActions.ExecuteMethod, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private CountTeams()
        { /* require use of factory methods */ }

        /// <summary>
        /// Counts the roots grouped by the number of their items.
        /// </summary>
        /// <param name="criteria">The criteria of the command.</param>
        /// <returns>The count list.</returns>
        public static async Task<CountTeamsList> Execute(
            CountTeamsCriteria criteria
            )
        {
            CountTeams command = new CountTeams();
            command.TeamName = criteria.TeamName;
            command.Result = null;

            //command.Validate();

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
                ICountTeamsDal dal = ctx.GetProvider<ICountTeamsDal>();
                CountTeamsCriteria criteria = new CountTeamsCriteria(TeamName);
                List<CountTeamsListItemDao> list = dal.Execute(criteria);

                // Set new data.
                Result = CountTeamsList.Get(list);
            }
        }

        #endregion
    }
}
