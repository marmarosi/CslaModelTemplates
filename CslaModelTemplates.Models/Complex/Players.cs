using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Contracts.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.Complex
{
    /// <summary>
    /// Represents an editable player collection.
    /// </summary>
    [Serializable]
    public class Players : EditableList<Players, Player>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(Players),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable player collection from the data transfer objects.
        /// </summary>
        /// <param name="list">The list of data transfer objects.</param>
        internal async Task Update(
            List<PlayerDto> list
            )
        {
            await Update(list, "PlayerKey");
        }

        #endregion

        #region Factory Methods

        private Players()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates a new editable player collection.
        /// </summary>
        /// <returns>The editable player collection.</returns>
        internal static Players Create()
        {
            return DataPortal.CreateChild<Players>();
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<PlayerDao> items
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // Create items from data access objects.
            foreach (PlayerDao dao in items)
                Add(DataPortal.FetchChild<Player>(dao));

            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
