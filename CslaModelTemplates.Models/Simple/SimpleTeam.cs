using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.Simple
{
    /// <summary>
    /// Represents an editable team object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "SimpleTeam")]
    public class SimpleTeam : EditableModel<SimpleTeam>
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

        protected override void AddBusinessRules()
        {
            // Call base class implementation to add data annotation rules to BusinessRules.
            // NOTE: DataAnnotation rules is always added with Priority = 0.
            base.AddBusinessRules();

            //// Add validation rules.
            //BusinessRules.AddRule(new Required(TeamNameProperty));

            //// Add authorization rules.
            //BusinessRules.AddRule(new IsInRole(
            //    AuthorizationActions.WriteProperty, TeamNameProperty, "Manager"));
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(SimpleTeam),
        //        new IsInRole(AuthorizationActions.EditObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        #endregion

        #region Factory Methods

        private SimpleTeam()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates a new editable team instance.
        /// </summary>
        /// <returns>The new editable team instance.</returns>
        public static async Task<SimpleTeam> Create()
        {
            return await DataPortal.CreateAsync<SimpleTeam>();
        }

        /// <summary>
        /// Gets an existing editable team instance.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        /// <returns>The requested editable team instance.</returns>
        public static async Task<SimpleTeam> Get(
            SimpleTeamCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<SimpleTeam>(criteria);
        }

        /// <summary>
        /// Deletes an existing team.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        public static async void Delete(
            SimpleTeamCriteria criteria
            )
        {
            await DataPortal.DeleteAsync<SimpleTeam>(criteria);
        }

        /// <summary>
        /// Rebuilds an editable team instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable team instance.</returns>
        public static async Task<SimpleTeam> FromDto(
            SimpleTeamDto dto
            )
        {
            SimpleTeam team = dto.TeamKey.HasValue ?
                await DataPortal.FetchAsync<SimpleTeam>(new SimpleTeamCriteria()
                {
                    TeamKey = dto.TeamKey.Value
                }) :
                await DataPortal.CreateAsync<SimpleTeam>();

            //team.TeamKey = dto.TeamKey;
            team.TeamCode = dto.TeamCode;
            team.TeamName = dto.TeamName;
            //team.Timestamp = dto.Timestamp;

            team.BusinessRules.CheckRules();
            return team;
        }

        #endregion

        #region Data Access

        //[RunLocal]
        //protected override void DataPortal_Create()
        //{
        //    // Load default values.
        //    // Omit this override if you have no defaults to set.
        //}

        private void DataPortal_Fetch(
            SimpleTeamCriteria criteria
            )
        {
            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleTeamDal dal = dm.GetProvider<ISimpleTeamDal>();
                SimpleTeamDao dao = dal.Fetch(criteria);

                using (BypassPropertyChecks)
                {
                    TeamKey = dao.TeamKey;
                    TeamCode = dao.TeamCode;
                    TeamName = dao.TeamName;
                    Timestamp = dao.Timestamp;
                }
            }
        }

        private SimpleTeamDao CreateDao()
        {
            // Build the data access object.
            return new SimpleTeamDao
            {
                TeamKey = TeamKey,
                TeamCode = TeamCode,
                TeamName = TeamName,
                Timestamp = Timestamp
            };
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            // Insert values into persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleTeamDal dal = dm.GetProvider<ISimpleTeamDal>();

                using (BypassPropertyChecks)
                {
                    SimpleTeamDao dao = CreateDao();
                    dal.Insert(dao);

                    // Set new data.
                    TeamKey = dao.TeamKey;
                    Timestamp = dao.Timestamp;
                }
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // Update values in persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleTeamDal dal = dm.GetProvider<ISimpleTeamDal>();

                using (BypassPropertyChecks)
                {
                    SimpleTeamDao dao = CreateDao();
                    dal.Update(dao);

                    // Set new data.
                    Timestamp = dao.Timestamp;
                }
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            if (TeamKey.HasValue)
                using (BypassPropertyChecks)
                    DataPortal_Delete(new SimpleTeamCriteria(TeamKey.Value));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SimpleTeamCriteria criteria)
        {
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleTeamDal dal = dm.GetProvider<ISimpleTeamDal>();

                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
