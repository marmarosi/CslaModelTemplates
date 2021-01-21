using Csla;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.SimpleView;
using System;

namespace CslaModelTemplates.Models.SimpleView
{
    /// <summary>
    /// Represents a read-only root object.
    /// </summary>
    [Serializable]
    public class RootView : ReadOnlyModel<RootView>
    {
        #region Business Methods

        public static readonly PropertyInfo<long?> RootKeyProperty = RegisterProperty<long?>(c => c.RootKey);
        public long? RootKey
        {
            get { return GetProperty(RootKeyProperty); }
            private set { LoadProperty(RootKeyProperty, value); }
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
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the specified read-only root object.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only root object.</param>
        public static RootView Get(
            RootViewCriteria criteria
            )
        {
            return DataPortal.Fetch<RootView>(criteria);
        }

        private RootView()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            RootViewCriteria criteria
            )
        {
            // Load values.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootViewDal dal = dm.GetProvider<IRootViewDal>();
                RootViewDao dao = dal.Fetch(criteria);

                RootKey = dao.RootKey;
                RootName = dao.RootName;
            }
        }

        #endregion
    }
}
