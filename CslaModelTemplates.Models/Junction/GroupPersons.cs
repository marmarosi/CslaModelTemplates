using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Contracts.Junction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.Junction
{
    /// <summary>
    /// Represents an editable group-person collection.
    /// </summary>
    [Serializable]
    public class GroupPersons : EditableList<GroupPersons, GroupPerson>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(GroupPersons),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Rebuilds an editable group-person collection from the data transfer objects.
        /// </summary>
        /// <param name="list">The list of data transfer objects.</param>
        /// <returns>The rebuilt editable group-person collection.</returns>
        internal async Task Update(
            List<GroupPersonDto> list
            )
        {
            List<int> indeces = Enumerable.Range(0, list.Count).ToList();
            for (int i = Items.Count - 1; i > -1; i--)
            {
                GroupPerson item = Items[i];
                GroupPersonDto dto = list.Find(o => o.PersonKey == item.PersonKey);
                if (dto == null)
                    RemoveItem(i);
                else
                {
                    item.Update(dto);
                    indeces.Remove(list.IndexOf(dto));
                }
            }
            foreach (int index in indeces)
                Items.Add(await GroupPerson.Create(this, list[index]));
        }

        #endregion

        #region Factory Methods

        private GroupPersons()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates a new editable group-person collection.
        /// </summary>
        /// <returns>The editable group-person collection.</returns>
        internal static GroupPersons Create()
        {
            return DataPortal.CreateChild<GroupPersons>();
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            List<GroupPersonDao> items
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // Create items from data access objects.
            foreach (GroupPersonDao dao in items)
                Add(DataPortal.FetchChild<GroupPerson>(dao));

            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
