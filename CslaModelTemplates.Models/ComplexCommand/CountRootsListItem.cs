using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.ComplexCommand;
using System;

namespace CslaModelTemplates.Models.Command
{
    /// <summary>
    /// Represents an item in a read-only root item collection.
    /// </summary>
    [Serializable]
    public class CountRootsListItem : ReadOnlyModel<CountRootsListItem>
    {
        #region Properties

        public static readonly PropertyInfo<int> ItemCountProperty = RegisterProperty<int>(c => c.ItemCount);
        public int ItemCount
        {
            get { return GetProperty(ItemCountProperty); }
            private set { LoadProperty(ItemCountProperty, value); }
        }

        public static readonly PropertyInfo<int> CountOfRootsProperty = RegisterProperty<int>(c => c.CountOfRoots);
        public int CountOfRoots
        {
            get { return GetProperty(CountOfRootsProperty); }
            private set { LoadProperty(CountOfRootsProperty, value); }
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.WriteProperty, ItemCountProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountRootsListItem),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private CountRootsListItem()
        { /* require use of factory methods */ }

        internal static CountRootsListItem Get(
            CountRootsListItemDao dao
            )
        {
            return DataPortal.FetchChild<CountRootsListItem>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            CountRootsListItemDao dao
            )
        {
            // Set values from data access object.
            ItemCount = dao.ItemCount;
            CountOfRoots = dao.CountOfRoots;
        }

        #endregion
    }
}
