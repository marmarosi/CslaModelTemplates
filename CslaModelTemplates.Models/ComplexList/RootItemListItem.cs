using Csla;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.ComplexList;
using System;

namespace CslaModelTemplates.Models.ComplexList
{
    /// <summary>
    /// Represents an item in a read-only root item collection.
    /// </summary>
    [Serializable]
    public class RootItemListItem : ReadOnlyModel<RootItemListItem>
    {
        #region Business Methods

        public static readonly PropertyInfo<long?> RootItemKeyProperty = RegisterProperty<long?>(c => c.RootItemKey);
        public long? RootItemKey
        {
            get { return GetProperty(RootItemKeyProperty); }
            private set { LoadProperty(RootItemKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> RootItemCodeProperty = RegisterProperty<string>(c => c.RootItemCode);
        public string RootItemCode
        {
            get { return GetProperty(RootItemCodeProperty); }
            private set { LoadProperty(RootItemCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> RootItemNameProperty = RegisterProperty<string>(c => c.RootItemName);
        public string RootItemName
        {
            get { return GetProperty(RootItemNameProperty); }
            private set { LoadProperty(RootItemNameProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // Add authorization rules.
            //BusinessRules.AddRule(...);
        }

        private static void AddObjectAuthorizationRules()
        {
            // Add authorization rules.
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        internal static RootItemListItem Get(
            RootItemListItemDao dao
            )
        {
            return DataPortal.FetchChild<RootItemListItem>(dao);
        }

        private RootItemListItem()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void Child_Fetch(
            RootItemListItemDao dao
            )
        {
            // Set values from data access object.
            RootItemKey = dao.RootItemKey;
            RootItemCode = dao.RootItemCode;
            RootItemName = dao.RootItemName;
        }

        #endregion
    }
}
