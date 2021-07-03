using Csla;
using CslaModelTemplates.Contracts;
using System;

namespace CslaModelTemplates.CslaExtensions.Models
{
    /// <summary>
    /// Represents a key-name option in a read-only choice object.
    /// </summary>
    [Serializable]
    public class KeyNameOption : ReadOnlyModel<KeyNameOption>
    {
        #region Business Methods

        public static readonly PropertyInfo<long?> KeyProperty = RegisterProperty<long?>(c => c.Key);
        public long? Key
        {
            get { return GetProperty(KeyProperty); }
            private set { LoadProperty(KeyProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
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

        /// <summary>
        /// Gets a key-name option.
        /// </summary>
        /// <param name="dao">The data access object of the key-name option.</param>
        /// <returns>The requested key-name option instance.</returns>
        public static KeyNameOption Get(
            KeyNameOptionDao dao
            )
        {
            return DataPortal.FetchChild<KeyNameOption>(dao);
        }

        private KeyNameOption()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void Child_Fetch(
            KeyNameOptionDao dao
            )
        {
            Key = dao.Key;
            Name = dao.Name;
        }

        #endregion
    }
}
