using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SimpleView;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SimpleView
{
    /// <summary>
    /// Represents a read-only root object.
    /// </summary>
    [Serializable]
    public class SimpleRootView : ReadOnlyModel<SimpleRootView>
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
        //        typeof(SimpleRootView),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private SimpleRootView()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets the specified read-only root instance.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only root.</param>
        /// <returns>The requested read-only root instance.</returns>
        public static async Task<SimpleRootView> Get(
            SimpleRootViewCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<SimpleRootView>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            SimpleRootViewCriteria criteria
            )
        {
            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleRootViewDal dal = dm.GetProvider<ISimpleRootViewDal>();
                SimpleRootViewDao dao = dal.Fetch(criteria);

                RootKey = dao.RootKey;
                RootCode = dao.RootCode;
                RootName = dao.RootName;
            }
        }

        #endregion
    }
}
