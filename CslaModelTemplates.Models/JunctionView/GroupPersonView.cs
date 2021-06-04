using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.JunctionView;
using System;

namespace CslaModelTemplates.Models.JunctionView
{
    /// <summary>
    /// Represents an item in a read-only group-person collection.
    /// </summary>
    [Serializable]
    public class GroupPersonView : ReadOnlyModel<GroupPersonView>
    {
        #region Properties

        public static readonly PropertyInfo<long?> PersonKeyProperty = RegisterProperty<long?>(c => c.PersonKey);
        public long? PersonKey
        {
            get { return GetProperty(PersonKeyProperty); }
            private set { LoadProperty(PersonKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> PersonNameProperty = RegisterProperty<string>(c => c.PersonName);
        public string PersonName
        {
            get { return GetProperty(PersonNameProperty); }
            private set { LoadProperty(PersonNameProperty, value); }
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.ReadProperty, PersonNameProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(GroupPersonView),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private GroupPersonView()
        { /* require use of factory methods */ }

        internal static GroupPersonView Get(
            GroupPersonViewDao dao
            )
        {
            return DataPortal.FetchChild<GroupPersonView>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            GroupPersonViewDao dao
            )
        {
            // Set values from data access object.
            PersonKey = dao.PersonKey;
            PersonName = dao.PersonName;
        }

        #endregion
    }
}
