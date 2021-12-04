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
using System.Linq;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.ComplexSet
{
    /// <summary>
    /// Represents an editable player object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "Player")]
    public class TeamSetPlayer : EditableModel<TeamSetPlayer, TeamSetPlayerDto>
    {
        #region Properties

        private long? PlayerKey
        {
            get { return KeyHash.Decode(ID.Player, PlayerId); }
            set { PlayerId = KeyHash.Encode(ID.Player, value); }
        }

        public static readonly PropertyInfo<string> PlayerIdProperty = RegisterProperty<string>(c => c.PlayerId);
        public string PlayerId
        {
            get { return GetProperty(PlayerIdProperty); }
            private set { SetProperty(PlayerIdProperty, value); }
        }

        private long? TeamKey
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

        public static readonly PropertyInfo<string> PlayerCodeProperty = RegisterProperty<string>(c => c.PlayerCode);
        [Required]
        [MaxLength(10)]
        public string PlayerCode
        {
            get { return GetProperty(PlayerCodeProperty); }
            set { SetProperty(PlayerCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> PlayerNameProperty = RegisterProperty<string>(c => c.PlayerName);
        [Required]
        [MaxLength(100)]
        public string PlayerName
        {
            get { return GetProperty(PlayerNameProperty); }
            set { SetProperty(PlayerNameProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // Call base class implementation to add data annotation rules to BusinessRules.
            // NOTE: DataAnnotation rules is always added with Priority = 0.
            base.AddBusinessRules();

            //// Add validation rules.
            //BusinessRules.AddRule(new Required(PlayerCodeProperty));
            BusinessRules.AddRule(new UniquePlayerCodes(PlayerCodeProperty));

            //// Add authorization rules.
            //BusinessRules.AddRule(new IsInRole(
            //    AuthorizationActions.WriteProperty, PlayerCodeProperty, "Manager"));
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamSetPlayer),
        //        new IsInRole(AuthorizationActions.EditObject, "Manager")
        //        );
        //}

        private class UniquePlayerCodes : BusinessRule
        {
            // Add additional parameters to your rule to the constructor,
            public UniquePlayerCodes(
                IPropertyInfo primaryProperty
                )
              : base(primaryProperty)
            {
                // If you are  going to add InputProperties make sure to
                // uncomment line below as InputProperties is NULL by default.
                //if (InputProperties == null) InputProperties = new List<IPropertyInfo>();

                // Add additional constructor code here.

                // Marke rule for IsAsync if Execute contains asyncronous code IsAsync = true; 
            }

            protected override void Execute(IRuleContext context)
            {
                TeamSetPlayer target = (TeamSetPlayer)context.Target;
                if (target.Parent == null)
                    return;

                TeamSetItem team = (TeamSetItem)target.Parent.Parent;
                var count = team.Players.Count(item => item.PlayerCode == target.PlayerCode);
                if (count > 1)
                    context.AddErrorResult(ValidationText.Player_PlayerCode_NotUnique);
            }
        }

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable player from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer objects.</param>
        public override async Task Update(
            TeamSetPlayerDto dto
            )
        {
            //PlayerKey = KeyHash.Decode(ID.Player, dto.PlayerId);
            //TeamKey = KeyHash.Decode(ID.Team, dto.TeamId);
            PlayerCode = dto.PlayerCode;
            PlayerName = dto.PlayerName;

            await base.Update(dto);
        }

        #endregion

        #region Factory Methods

        private TeamSetPlayer()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates an editable player instance from the data transfer object.
        /// </summary>
        /// <param name="parent">The parent collection.</param>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The new editable player instance.</returns>
        internal static new async Task<TeamSetPlayer> Create(
            IParent parent,
            TeamSetPlayerDto dto
            )
        {
            return await Create(parent, dto);
        }

        #endregion

        #region Data Access

        //[RunLocal]
        //protected override void Child_Create()
        //{
        //    // Load default values.
        //    // Omit this override if you have no defaults to set.
        //}

        private void Child_Fetch(
            TeamSetPlayerDao dao
            )
        {
            using (BypassPropertyChecks)
            {
                // Set values from data access object.
                PlayerKey = dao.PlayerKey;
                TeamKey = dao.TeamKey;
                PlayerCode = dao.PlayerCode;
                PlayerName = dao.PlayerName;
            }
        }

        private TeamSetPlayerDao CreateDao()
        {
            // Build the data access object.
            return new TeamSetPlayerDao
            {
                PlayerKey = PlayerKey,
                TeamKey = TeamKey,
                PlayerCode = PlayerCode,
                PlayerName = PlayerName,
                __teamCode = ((TeamSetItem)Parent.Parent).TeamCode
            };
        }

        private void Child_Insert(
            TeamSetItem parent
            )
        {
            // Insert values into persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamSetPlayerDal dal = dm.GetProvider<ITeamSetPlayerDal>();

                using (BypassPropertyChecks)
                {
                    TeamKey = parent.TeamKey;
                    TeamSetPlayerDao dao = CreateDao();
                    dal.Insert(dao);

                    // Set new data.
                    PlayerKey = dao.PlayerKey;
                }
                //FieldManager.UpdateChildren(this);
            }
        }

        private void Child_Update(
            TeamSetItem parent
            )
        {
            // Update values in persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamSetPlayerDal dal = dm.GetProvider<ITeamSetPlayerDal>();

                using (BypassPropertyChecks)
                {
                    TeamSetPlayerDao dao = CreateDao();
                    dal.Update(dao);

                    // Set new data.
                }
                //FieldManager.UpdateChildren(this);
            }
        }

        private void Child_DeleteSelf(
            TeamSetItem parent
            )
        {
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ITeamSetPlayerDal dal = dm.GetProvider<ITeamSetPlayerDal>();

                //Items.Clear();
                //FieldManager.UpdateChildren(this);

                TeamSetPlayerCriteria criteria = new TeamSetPlayerCriteria(PlayerKey.Value)
                {
                    __teamCode = ((TeamSetItem)Parent.Parent).TeamCode,
                    __playerCode = PlayerCode
                };
                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
