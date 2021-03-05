using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.LookUpView;
using System;

namespace CslaModelTemplates.Models.LookUpView
{
    /// <summary>
    /// Represents an item in a read-only member collection.
    /// </summary>
    [Serializable]
    public class MemberView : ReadOnlyModel<MemberView>
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
        //        typeof(MemberView),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private MemberView()
        { /* require use of factory methods */ }

        internal static MemberView Get(
            MemberViewDao dao
            )
        {
            return DataPortal.FetchChild<MemberView>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            MemberViewDao dao
            )
        {
            // Set values from data access object.
            PersonKey = dao.PersonKey;
            PersonName = dao.PersonName;
        }

        #endregion
    }
}
