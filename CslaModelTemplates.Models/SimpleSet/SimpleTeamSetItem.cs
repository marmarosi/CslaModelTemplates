using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SimpleSet
{
    /// <summary>
    /// Represents an editable child object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "SimpleTeamSetItem")]
    public class SimpleTeamSetItem : EditableModel<SimpleTeamSetItem>
    {
        #region Properties

        public static readonly PropertyInfo<long?> TeamKeyProperty = RegisterProperty<long?>(c => c.TeamKey);
        public long? TeamKey
        {
            get { return GetProperty(TeamKeyProperty); }
            private set { LoadProperty(TeamKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> TeamCodeProperty = RegisterProperty<string>(c => c.TeamCode);
        [Required]
        [MaxLength(10)]
        public string TeamCode
        {
            get { return GetProperty(TeamCodeProperty); }
            set { SetProperty(TeamCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> TeamNameProperty = RegisterProperty<string>(c => c.TeamName);
        [Required]
        [MaxLength(100)]
        public string TeamName
        {
            get { return GetProperty(TeamNameProperty); }
            set { SetProperty(TeamNameProperty, value); }
        }

        public static readonly PropertyInfo<DateTime?> TimestampProperty = RegisterProperty<DateTime?>(c => c.Timestamp);
        public DateTime? Timestamp
        {
            get { return GetProperty(TimestampProperty); }
            private set { LoadProperty(TimestampProperty, value); }
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add validation rules.
        //    BusinessRules.AddRule(new Required(TeamNameProperty));

        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.WriteProperty, TeamNameProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(SimpleTeamSetItem),
        //        new IsInRole(AuthorizationActions.EditObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Gets an existing editable team instance.
        /// </summary>
        /// <param name="dao">The data access objects.</param>
        /// <returns>The requested editable team instance.</returns>
        internal static SimpleTeamSetItem Get(
            SimpleTeamSetItemDao dao
            )
        {
            return DataPortal.FetchChild<SimpleTeamSetItem>(dao);
        }

        /// <summary>
        /// Updates an editable team from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer objects.</param>
        internal void Update(
            SimpleTeamSetItemDto dto
            )
        {
            //TeamKey = dto.TeamKey;
            TeamCode = dto.TeamCode;
            TeamName = dto.TeamName;
            //Timestamp = dto.Timestamp;
        }

        #endregion

        #region Factory Methods

        private SimpleTeamSetItem()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates an editable team instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The new editable team instance.</returns>
        internal static async Task<SimpleTeamSetItem> Create(
            SimpleTeamSetItemDto dto
            )
        {
            SimpleTeamSetItem team = null;
            await Task.Run(() => team = DataPortal.CreateChild<SimpleTeamSetItem>());
            team.Update(dto);
            return team;
        }

        #endregion

        #region Data Access

        //protected override void Child_Create()
        //{
        //    // TODO: load default values
        //    // omit this override if you have no defaults to set
        //}

        private void Child_Fetch(
            SimpleTeamSetItemDao dao
            )
        {
            using (BypassPropertyChecks)
            {
                // Set values from data access object.
                TeamKey = dao.TeamKey;
                TeamCode = dao.TeamCode;
                TeamName = dao.TeamName;
                Timestamp = dao.Timestamp;
            }
        }

        private SimpleTeamSetItemDao CreateDao()
        {
            // Build the data access object.
            return new SimpleTeamSetItemDao
            {
                TeamKey = TeamKey,
                TeamCode = TeamCode,
                TeamName = TeamName,
                Timestamp = Timestamp
            };
        }

        private void Child_Insert()
        {
            // Insert values into persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleTeamSetItemDal dal = dm.GetProvider<ISimpleTeamSetItemDal>();

                using (BypassPropertyChecks)
                {
                    SimpleTeamSetItemDao dao = CreateDao();
                    dal.Insert(dao);

                    // Set new data.
                    TeamKey = dao.TeamKey;
                    Timestamp = dao.Timestamp;
                }
            }
        }

        private void Child_Update()
        {
            // Update values in persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleTeamSetItemDal dal = dm.GetProvider<ISimpleTeamSetItemDal>();

                using (BypassPropertyChecks)
                {
                    SimpleTeamSetItemDao dao = CreateDao();
                    dal.Update(dao);

                    // Set new data.
                    Timestamp = dao.Timestamp;
                }
            }
        }

        private void Child_DeleteSelf()
        {
            // TODO: delete values
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleTeamSetItemDal dal = dm.GetProvider<ISimpleTeamSetItemDal>();

                SimpleTeamSetItemCriteria criteria = new SimpleTeamSetItemCriteria(TeamKey.Value);
                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
