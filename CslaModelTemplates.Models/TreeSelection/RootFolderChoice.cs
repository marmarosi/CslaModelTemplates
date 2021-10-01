using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.TreeSelection;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.TreeSelection
{
    /// <summary>
    /// Represents a read-only tree choice collection.
    /// </summary>
    [Serializable]
    public class RootFolderChoice : ReadOnlyList<RootFolderChoice, IdNameOption>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamIdChoice),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private RootFolderChoice()
        { /* require use of factory methods */ }

        /// <summary>
        /// Gets a choice of tree options.
        /// </summary>
        /// <returns>The requested tree choice instance.</returns>
        public static async Task<RootFolderChoice> Get()
        {
            return await DataPortal.FetchAsync<RootFolderChoice>(new RootFolderChoiceCriteria());
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
            RootFolderChoiceCriteria criteria
            )
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootFolderChoiceDal dal = dm.GetProvider<IRootFolderChoiceDal>();
                List<IdNameOptionDao> choice = dal.Fetch(criteria);

                foreach (IdNameOptionDao dao in choice)
                    Add(IdNameOption.Get(dao, ID.Folder));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
