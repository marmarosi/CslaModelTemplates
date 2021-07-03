using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.PaginatedSortedList;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Dal;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.PaginatedSortedList
{
    /// <summary>
    /// Represents a read-only paginated sorted team collection.
    /// </summary>
    [Serializable]
    public class PaginatedSortedTeamList : ReadOnlyModel<PaginatedSortedTeamList>
    {
        #region Properties

        public static readonly PropertyInfo<PaginatedSortedTeamListItems> PlayersProperty = RegisterProperty<PaginatedSortedTeamListItems>(c => c.Data);
        public PaginatedSortedTeamListItems Data
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
        //        AuthorizationActions.ReadProperty, Data, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PaginatedSortedTeamList),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private PaginatedSortedTeamList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets the specified read-only paginated sorted team collection.
        /// </summary>
        /// <param name="criteria">The criteria of the read-only team.</param>
        /// <returns>The requested read-only team instance.</returns>
        public static async Task<PaginatedSortedTeamList> Get(
            PaginatedSortedTeamListCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<PaginatedSortedTeamList>(criteria);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            PaginatedSortedTeamListCriteria criteria
            )
        {
            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IPaginatedSortedTeamListDal dal = dm.GetProvider<IPaginatedSortedTeamListDal>();
                IPaginatedList<PaginatedSortedTeamListItemDao> dao = dal.Fetch(criteria);

                Data = PaginatedSortedTeamListItems.Get(dao.Data);
                TotalCount = dao.TotalCount;
            }
        }

        #endregion
    }
}
