using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Dal.Contracts;
using System;

namespace CslaModelTemplates.CslaExtensions.Models
{
    /// <summary>
    /// Represents a key-name option in a read-only choice object.
    /// </summary>
    [Serializable]
    public class IdNameOption : ReadOnlyModel<IdNameOption>
    {
        #region Business Methods

        private string _hashid;

        public static readonly PropertyInfo<string> IdProperty = RegisterProperty<string>(c => c.Id, RelationshipTypes.PrivateField);
        private long? Key = null;
        public string Id
        {
            get { return GetProperty(IdProperty, KeyHash.Encode(_hashid, Key)); }
            private set { Key = KeyHash.Decode(_hashid, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.ReadProperty, IdProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(KeyNameOption),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets an ID-name option.
        /// </summary>
        /// <param name="dao">The data access object of the ID-name option.</param>
        /// <returns>The requested ID-name option instance.</returns>
        public static IdNameOption Get(
            IdNameOptionDao dao,
            string model
            )
        {
            IdNameOption option = DataPortal.FetchChild<IdNameOption>(dao);
            option._hashid = model;
            return option;
        }

        private IdNameOption()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void Child_Fetch(
            IdNameOptionDao dao
            )
        {
            Key = dao.Key;
            Name = dao.Name;
        }

        #endregion
    }
}
