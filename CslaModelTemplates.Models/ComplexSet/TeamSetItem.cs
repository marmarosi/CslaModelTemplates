using Csla;
using Csla.Core;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.ComplexSet;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.CslaExtensions.Validations;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.ComplexSet
{
    /// <summary>
    /// Represents an editable child object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "TeamSetItem")]
    public class TeamSetItem : EditableModel<TeamSetItem, TeamSetItemDto>
    {
        #region Properties

        internal long? TeamKey
        {
            get { return KeyHash.Decode(ID.Team, TeamId); }
            set { TeamId = KeyHash.Encode(ID.Team, value); }
        }

        public static readonly PropertyInfo<string> TeamIdProperty = RegisterProperty<string>(c => c.TeamId);
        public string TeamId
        {
            get { return GetProperty(TeamIdProperty); }
            private set { SetProperty(TeamIdProperty, value); }
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

        public static readonly PropertyInfo<TeamSetPlayers> PlayersProperty = RegisterProperty<TeamSetPlayers>(c => c.Players);
        public TeamSetPlayers Players
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
        //        typeof(TeamSetItem),
        //        new IsInRole(AuthorizationActions.EditObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable team from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        public override async Task Update(
            TeamSetItemDto dto
            )
        {
            //TeamKey = KeyHash.Decode(ID.Team, dto.TeamId);
            TeamCode = dto.TeamCode;
            TeamName = dto.TeamName;
            await Players.Update(dto.Players);
            //Timestamp = dto.Timestamp;

            await base.Update(dto);
        }

        #endregion

        #region Factory Methods

        private TeamSetItem()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Gets an existing editable team instance.
        /// </summary>
        /// <param name="dao">The data access objects.</param>
        /// <returns>The requested editable team instance.</returns>
        internal static TeamSetItem Get(
            TeamSetItemDao dao
            )
        {
            return DataPortal.FetchChild<TeamSetItem>(dao);
        }

        /// <summary>
        /// Creates an editable team instance from the data transfer object.
        /// </summary>
        /// <param name="parent">The parent collection.</param>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The new editable team instance.</returns>
        internal static new async Task<TeamSetItem> Create(
            IParent parent,
            TeamSetItemDto dto
            )
        {
            return await Create(parent, dto);
        }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void Child_Create()
        {
            // Load default values.
            // Omit this override if you have no defaults to set.
            Players = TeamSetPlayers.Create();
        }

        private void Child_Fetch(
            TeamSetItemDao dao
            )
        {
            using (BypassPropertyChecks)
            {
                // Set values from data access object.
                TeamKey = dao.TeamKey;
                TeamCode = dao.TeamCode;
                TeamName = dao.TeamName;
                Players = DataPortal.FetchChild<TeamSetPlayers>(dao.Players);
                Timestamp = dao.Timestamp;
            }
        }

        private TeamSetItemDao CreateDao()
        {
            // Build the data access object.
            return new TeamSetItemDao
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
                ITeamSetItemDal dal = dm.GetProvider<ITeamSetItemDal>();

                using (BypassPropertyChecks)
                {
                    TeamSetItemDao dao = CreateDao();
                    dal.Insert(dao);

                    // Set new data.
                    TeamKey = dao.TeamKey;
                    Timestamp = dao.Timestamp;
                }
                FieldManager.UpdateChildren(this);
            }
        }

        private void Child_Update()
        {
            // Update values in persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                if (IsSelfDirty)
                {
                    ITeamSetItemDal dal = dm.GetProvider<ITeamSetItemDal>();

                    using (BypassPropertyChecks)
                    {
                        TeamSetItemDao dao = CreateDao();
                        dal.Update(dao);

                        // Set new data.
                        Timestamp = dao.Timestamp;
                    }
                }
                FieldManager.UpdateChildren(this);
            }
        }

        private void Child_DeleteSelf()
        {
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamSetItemDal dal = dm.GetProvider<ITeamSetItemDal>();

                Players.Clear();
                FieldManager.UpdateChildren(this);

                TeamSetItemCriteria criteria = new TeamSetItemCriteria(TeamKey.Value);
                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
