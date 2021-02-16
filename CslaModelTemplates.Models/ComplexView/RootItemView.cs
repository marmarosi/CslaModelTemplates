using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.ComplexView;
using System;

namespace CslaModelTemplates.Models.ComplexView
{
    /// <summary>
    /// Represents an item in a read-only root item collection.
    /// </summary>
    [Serializable]
    public class RootItemView : ReadOnlyModel<RootItemView>
    {
        #region Properties

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

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.ReadProperty, RootItemNameProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(RootItemView),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private RootItemView()
        { /* require use of factory methods */ }

        internal static RootItemView Get(
            RootItemViewDao dao
            )
        {
            return DataPortal.FetchChild<RootItemView>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            RootItemViewDao dao
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
