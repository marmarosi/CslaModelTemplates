using Csla;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.ComplexView;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.ComplexView
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

        public static readonly PropertyInfo<RootItemViews> ItemsProperty = RegisterProperty<RootItemViews>(c => c.Items);
        public RootItemViews Items
        {
            get { return GetProperty(ItemsProperty); }
            private set { LoadProperty(ItemsProperty, value); }
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
        /// Gets the specified read-only root instance.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only root.</param>
        /// <returns>The requested read-only root instance.</returns>
        public static RootView Get(
            RootViewCriteria criteria
            )
        {
            return DataPortal.Fetch<RootView>(criteria);
        }

        /// <summary>
        /// Gets the specified read-only root instance.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only root.</param>
        /// <returns>The requested read-only root instance.</returns>
        public static async Task<RootView> GetAsync(
            RootViewCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<RootView>(criteria);
        }

        private RootView()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            RootViewCriteria criteria
            )
        {
            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootViewDal dal = dm.GetProvider<IRootViewDal>();
                RootViewDao dao = dal.Fetch(criteria);

                RootKey = dao.RootKey;
                RootCode = dao.RootCode;
                RootName = dao.RootName;
                Items = RootItemViews.Get(dao.Items);
            }
        }

        #endregion
    }
}
