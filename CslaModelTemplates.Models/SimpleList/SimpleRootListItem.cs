using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SimpleList;
using System;

namespace CslaModelTemplates.Models.SimpleList
{
    /// <summary>
    /// Represents an item in a read-only root collection.
    /// </summary>
    [Serializable]
    public class SimpleRootListItem : ReadOnlyModel<SimpleRootListItem>
    {
        #region Properties

        public static readonly PropertyInfo<long?> RootKeyProperty = RegisterProperty<long?>(c => c.RootKey);
        public long? RootKey
        {
            get { return GetProperty(RootKeyProperty); }
            private set { LoadProperty(RootKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> RootCodeProperty = RegisterProperty<string>(c => c.RootCode);
        public string RootCode
        {
            get { return GetProperty(RootCodeProperty); }
            private set { LoadProperty(RootCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> RootNameProperty = RegisterProperty<string>(c => c.RootName);
        public string RootName
        {
            get { return GetProperty(RootNameProperty); }
            private set { LoadProperty(RootNameProperty, value); }
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.ReadProperty, RootNameProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(SimpleRootListItem),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private SimpleRootListItem()
        { /* require use of factory methods */ }

        internal static SimpleRootListItem Get(
            SimpleRootListItemDao dao
            )
        {
            return DataPortal.FetchChild<SimpleRootListItem>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            SimpleRootListItemDao dao
            )
        {
            // Set values from data access object.
            RootKey = dao.RootKey;
            RootCode = dao.RootCode;
            RootName = dao.RootName;
        }

        #endregion
    }
}
