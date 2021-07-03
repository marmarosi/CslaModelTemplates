using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Dal;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Contracts.JunctionView;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.JunctionView
{
    /// <summary>
    /// Represents a read-only group object.
    /// </summary>
    [Serializable]
    public class GroupView : ReadOnlyModel<GroupView>
    {
        #region Properties

        public static readonly PropertyInfo<long?> GroupKeyProperty = RegisterProperty<long?>(c => c.GroupKey);
        public long? GroupKey
        {
            get { return GetProperty(GroupKeyProperty); }
            private set { LoadProperty(GroupKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> GroupCodeProperty = RegisterProperty<string>(c => c.GroupCode);
        public string GroupCode
        {
            get { return GetProperty(GroupCodeProperty); }
            private set { LoadProperty(GroupCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> GroupNameProperty = RegisterProperty<string>(c => c.GroupName);
        public string GroupName
        {
            get { return GetProperty(GroupNameProperty); }
            private set { LoadProperty(GroupNameProperty, value); }
        }

        public static readonly PropertyInfo<GroupPersonViews> PersonsProperty = RegisterProperty<GroupPersonViews>(c => c.Persons);
        public GroupPersonViews Persons
        {
            get { return GetProperty(PersonsProperty); }
            private set { LoadProperty(PersonsProperty, value); }
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.ReadProperty, GroupNameProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(GroupView),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private GroupView()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets the specified read-only group instance.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only group.</param>
        /// <returns>The requested read-only group instance.</returns>
        public static async Task<GroupView> Get(
            GroupViewCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<GroupView>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            GroupViewCriteria criteria
            )
        {
            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IGroupViewDal dal = dm.GetProvider<IGroupViewDal>();
                GroupViewDao dao = dal.Fetch(criteria);

                GroupKey = dao.GroupKey;
                GroupCode = dao.GroupCode;
                GroupName = dao.GroupName;
                Persons = GroupPersonViews.Get(dao.Persons);
            }
        }

        #endregion
    }
}
