using Csla;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SimpleList;
using System;

namespace CslaModelTemplates.Models.SimpleList
{
    /// <summary>
    /// Represents an item in a read-only root collection.
    /// </summary>
    [Serializable]
    public class RootListItem : ReadOnlyModel<RootListItem>
    {
        #region Business Methods

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

        internal static RootListItem Get(
            RootListItemDao dao
            )
        {
            return DataPortal.FetchChild<RootListItem>(dao);
        }

        private RootListItem()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void Child_Fetch(
            RootListItemDao dao
            )
        {
            RootKey = dao.RootKey;
            RootCode = dao.RootCode;
            RootName = dao.RootName;
        }

        #endregion
    }
}
