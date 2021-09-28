using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.CslaExtensions.Validations;
using CslaModelTemplates.Contracts.Complex;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.Complex
{
    /// <summary>
    /// Represents an editable team object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "Team")]
    public class Team : EditableModel<Team>
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

        public static readonly PropertyInfo<Players> PlayersProperty = RegisterProperty<Players>(c => c.Players, RelationshipTypes.Child);
        public Players Players
        {
            get { return GetProperty(PlayersProperty); }
            private set { LoadProperty(PlayersProperty, value); }
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
        //        typeof(Team),
        //        new IsInRole(AuthorizationActions.EditObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable team from the data transfer object.
        /// </summary>
        /// <param name="data">The data transfer object.</param>
        public override async Task Update(
            object data
            )
        {
            TeamDto dto = data as TeamDto;
            using (BypassPropertyChecks)
            {
                //TeamKey = dto.TeamKey;
                TeamCode = dto.TeamCode;
                TeamName = dto.TeamName;
                await Players.Update(dto.Players);
                //Timestamp = dto.Timestamp;
            }
            await base.Update(data);
        }

        #endregion

        #region Factory Methods

        private Team()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates a new editable team instance.
        /// </summary>
        /// <returns>The new editable team instance.</returns>
        public static async Task<Team> Create()
        {
            return await DataPortal.CreateAsync<Team>();
        }

        /// <summary>
        /// Gets an existing editable team instance.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        /// <returns>The requested editable team instance.</returns>
        public static async Task<Team> Get(
            TeamCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<Team>(criteria);
        }

        /// <summary>
        /// Deletes an existing team.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        public static async void Delete(
            TeamCriteria criteria
            )
        {
            await DataPortal.DeleteAsync<Team>(criteria);
        }

        /// <summary>
        /// Rebuilds an editable team instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable team instance.</returns>
        public static async Task<Team> FromDto(
            TeamDto dto
            )
        {
            Team team = dto.TeamKey.HasValue ?
                await DataPortal.FetchAsync<Team>(new TeamCriteria()
                {
                    TeamKey = dto.TeamKey.Value
                }) :
                await DataPortal.CreateAsync<Team>();
            await team.Update(dto);
            return team;
        }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // Load default values.
            // Omit this override if you have no defaults to set.
            Players = Players.Create();
        }

        private void DataPortal_Fetch(
            TeamCriteria criteria
            )
        {
            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamDal dal = dm.GetProvider<ITeamDal>();
                TeamDao dao = dal.Fetch(criteria);

                using (BypassPropertyChecks)
                {
                    TeamKey = dao.TeamKey;
                    TeamCode = dao.TeamCode;
                    TeamName = dao.TeamName;
                    Players = DataPortal.FetchChild<Players>(dao.Players);
                    Timestamp = dao.Timestamp;
                }
            }
        }

        private TeamDao CreateDao()
        {
            // Build the data access object.
            return new TeamDao
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
                ITeamDal dal = dm.GetProvider<ITeamDal>();

                using (BypassPropertyChecks)
                {
                    TeamDao dao = CreateDao();
                    dal.Insert(dao);

                    // Set new data.
                    TeamKey = dao.TeamKey;
                    Timestamp = dao.Timestamp;
                }
                FieldManager.UpdateChildren(this);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // Update values in persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamDal dal = dm.GetProvider<ITeamDal>();

                using (BypassPropertyChecks)
                {
                    TeamDao dao = CreateDao();
                    dal.Update(dao);

                    // Set new data.
                    Timestamp = dao.Timestamp;
                }
                FieldManager.UpdateChildren(this);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            if (TeamKey.HasValue)
                using (BypassPropertyChecks)
                    DataPortal_Delete(new TeamCriteria(TeamKey.Value));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(TeamCriteria criteria)
        {
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamDal dal = dm.GetProvider<ITeamDal>();

                if (!TeamKey.HasValue)
                    DataPortal_Fetch(criteria);

                Players.Clear();
                FieldManager.UpdateChildren(this);

                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
