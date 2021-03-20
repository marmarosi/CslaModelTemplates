using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.LookUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.LookUp
{
    /// <summary>
    /// Represents an editable member collection.
    /// </summary>
    [Serializable]
    public class Members : EditableList<Members, Member>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(Members),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Creates a new editable player collection.
        /// </summary>
        /// <returns>The editable player collection.</returns>
        internal static Members Create()
        {
            return DataPortal.CreateChild<Members>();
        }

        /// <summary>
        /// Rebuilds an editable player collection from the data transfer objects.
        /// </summary>
        /// <param name="list">The list of data transfer objects.</param>
        /// <returns>The rebuilt editable player collection.</returns>
        internal async Task Update(
            List<MemberDto> list
            )
        {
            List<int> indeces = Enumerable.Range(0, list.Count).ToList();
            for (int i = Items.Count - 1; i > -1; i--)
            {
                Member item = Items[i];
                MemberDto dto = list.Find(o => o.PersonKey == item.PersonKey);
                if (dto == null)
                    RemoveItem(i);
                else
                {
                    item.Update(dto);
                    indeces.Remove(list.IndexOf(dto));
                }
            }
            foreach (int index in indeces)
                Items.Add(await Member.Create(list[index]));
        }

        #endregion

        #region Factory Methods

        private Members()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<MemberDao> items
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // Create items from data access objects.
            foreach (MemberDao dao in items)
                Add(DataPortal.FetchChild<Member>(dao));

            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
