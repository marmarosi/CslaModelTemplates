using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.DataTransfer;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Contracts.PaginatedList;
using CslaModelTemplates.Dal;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.PaginatedList
{
    /// <summary>
    /// Represents a read-only paginated team collection.
    /// </summary>
    [Serializable]
    public class PaginatedTeamList : ReadOnlyModel<PaginatedTeamList>
    {
        #region Properties

        public static readonly PropertyInfo<PaginatedTeamListItems> PlayersProperty = RegisterProperty<PaginatedTeamListItems>(c => c.Data);
        public PaginatedTeamListItems Data
        {
            get { return GetProperty(PlayersProperty); }
            private set { LoadProperty(PlayersProperty, value); }
        }

        public static readonly PropertyInfo<int> TotalCountProperty = RegisterProperty<int>(c => c.TotalCount);
        public int TotalCount
        {
            get { return GetProperty(TotalCountProperty); }
            private set { LoadProperty(TotalCountProperty, value); }
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.ReadProperty, TeamNameProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PaginatedTeamList),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private PaginatedTeamList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets the specified read-only team instance.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only team.</param>
        /// <returns>The requested read-only team instance.</returns>
        public static async Task<PaginatedTeamList> Get(
            PaginatedTeamListCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<PaginatedTeamList>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            PaginatedTeamListCriteria criteria
            )
        {
            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IPaginatedTeamListDal dal = dm.GetProvider<IPaginatedTeamListDal>();
                PaginatedList<PaginatedTeamListItemDao> dao = dal.Fetch(criteria);

                Data = PaginatedTeamListItems.Get(dao.Data);
                TotalCount = dao.TotalCount;
            }
        }

        #endregion
    }
}
