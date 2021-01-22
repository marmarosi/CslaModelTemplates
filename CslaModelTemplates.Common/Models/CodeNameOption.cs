using Csla;
using System;

namespace CslaModelTemplates.Common.Models
{
    /// <summary>
    /// Represents a code-name option in a read-only choice object.
    /// </summary>
    [Serializable]
    public class CodeNameOption : ReadOnlyModel<CodeNameOption>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(c => c.Code);
        public string Code
        {
            get { return GetProperty(CodeProperty); }
            private set { LoadProperty(CodeProperty, value); }
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
        /// Gets a code-name option.
        /// </summary>
        /// <param name="dao">The data access object of the code-name option.</param>
        /// <returns>The requested code-name option instance.</returns>
        public static CodeNameOption Get(
            CodeNameOptionDao dao
            )
        {
            return DataPortal.FetchChild<CodeNameOption>(dao);
        }

        private CodeNameOption()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void Child_Fetch(
            CodeNameOptionDao dao
            )
        {
            Code = dao.Code;
            Name = dao.Name;
        }

        #endregion
    }
}
