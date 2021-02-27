using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
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
        /// Creates a new editable player collection.
        /// </summary>
        /// <returns>The editable player collection.</returns>
        internal static TeamSetPlayers Create()
        {
            return DataPortal.CreateChild<TeamSetPlayers>();
        }

        /// <summary>
        /// Rebuilds an editable player collection from the data transfer objects.
        /// </summary>
        /// <param name="list">The list of data transfer objects.</param>
        /// <returns>The rebuilt editable player collection.</returns>
        internal async Task Update(
            List<TeamSetPlayerDto> list
            )
        {
            List<int> indeces = Enumerable.Range(0, list.Count).ToList();
            for (int i = Items.Count - 1; i > -1; i--)
            {
                TeamSetPlayer item = Items[i];
                TeamSetPlayerDto dto = list.Find(o => o.PlayerKey == item.PlayerKey);
                if (dto == null)
                    RemoveItem(i);
                else
                {
                    item.Update(dto);
                    indeces.Remove(list.IndexOf(dto));
                }
            }
            foreach (int index in indeces)
                Items.Add(await TeamSetPlayer.Create(list[index]));
        }

        #endregion

        #region Factory Methods

        private TeamSetPlayers()
        { }

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
