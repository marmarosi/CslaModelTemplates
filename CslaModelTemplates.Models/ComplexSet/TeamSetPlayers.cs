using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Contracts.ComplexSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.ComplexSet
{
    /// <summary>
    /// Represents an editable player collection.
    /// </summary>
    [Serializable]
    public class TeamSetPlayers : EditableList<TeamSetPlayers, TeamSetPlayer>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamSetPlayers),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Rebuilds an editable player collection from the data transfer objects.
        /// </summary>
        /// <param name="list">The list of data transfer objects.</param>
        internal async Task Update(
            List<TeamSetPlayerDto> list
            )
        {
            await Update(list, "PlayerKey");
        }

        #endregion

        #region Factory Methods

        private TeamSetPlayers()
        { }

        /// <summary>
        /// Creates a new editable player collection.
        /// </summary>
        /// <returns>The editable player collection.</returns>
        internal static TeamSetPlayers Create()
        {
            return DataPortal.CreateChild<TeamSetPlayers>();
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<TeamSetPlayerDao> items
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // Create items from data access objects.
            foreach (TeamSetPlayerDao dao in items)
                Add(DataPortal.FetchChild<TeamSetPlayer>(dao));

            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
