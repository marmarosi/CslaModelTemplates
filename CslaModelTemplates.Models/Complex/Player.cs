using Csla;
using Csla.Core;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.Complex;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.Complex
{
    /// <summary>
    /// Represents an editable player object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "Player")]
    public class Player : EditableModel<Player>
    {
        #region Properties

        public static readonly PropertyInfo<long?> PlayerKeyProperty = RegisterProperty<long?>(c => c.PlayerKey);
        public long? PlayerKey
        {
            get { return GetProperty(PlayerKeyProperty); }
            private set { LoadProperty(PlayerKeyProperty, value); }
        }

        public static readonly PropertyInfo<long?> TeamKeyProperty = RegisterProperty<long?>(c => c.TeamKey);
        public long? TeamKey
        {
            get { return GetProperty(TeamKeyProperty); }
            private set { LoadProperty(TeamKeyProperty, value); }
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
            // Add validation rules.
            BusinessRules.AddRule(new UniquePlayerCodes(PlayerCodeProperty));

            // Add authorization rules.
            //BusinessRules.AddRule(new IsInRole(
            //    AuthorizationActions.WriteProperty, PlayerCodeProperty, "Manager"));
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(Player),
        //        new IsInRole(AuthorizationActions.EditObject, "Manager")
        //        );
        //}

        private class UniquePlayerCodes : BusinessRule
        {
            // TODO: Add additional parameters to your rule to the constructor
            public UniquePlayerCodes(
                IPropertyInfo primaryProperty
                )
              : base(primaryProperty)
            {
                // TODO: If you are  going to add InputProperties make sure to
                // uncomment line below as InputProperties is NULL by default.
                //if (InputProperties == null) InputProperties = new List<IPropertyInfo>();

                // TODO: Add additional constructor code here 

                // TODO: Marke rule for IsAsync if Execute contains asyncronous code 
                // IsAsync = true; 
            }

            protected override void Execute(IRuleContext context)
            {
                // TODO: Add actual rule code here. 
                //if (broken condition)
                //{
                //  context.AddErrorResult("Broken rule message");
                //}
                Player target = (Player)context.Target;
                if (target.Parent == null)
                    return;

                Team team = (Team)target.Parent.Parent;
                var count = team.Players.Count(item => item.PlayerCode == target.PlayerCode);
                if (count > 1)
                    context.AddErrorResult(ValidationText.Player_PlayerCode_NotUnique);
            }
        }

        #endregion

        #region Business Methods

        /// <summary>
        /// Creates an editable player instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The new editable player instance.</returns>
        internal static async Task<Player> Create(
            PlayerDto dto
            )
        {
            Player item = null;
            item = await Task.Run(() => DataPortal.CreateChild<Player>());
            item.Update(dto);
            return item;
        }

        /// <summary>
        /// Updates an editable player from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer objects.</param>
        internal void Update(
            PlayerDto dto
            )
        {
            //PlayerKey = dto.PlayerKey;
            //TeamKey = dto.TeamKey;
            PlayerCode = dto.PlayerCode;
            PlayerName = dto.PlayerName;
        }

        #endregion

        #region Factory Methods

        private Player()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        //protected override void Child_Create()
        //{
        //    // TODO: load default values
        //    // omit this override if you have no defaults to set
        //}

        private void Child_Fetch(
            PlayerDao dao
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

        private PlayerDao CreateDao()
        {
            // Build the data access object.
            return new PlayerDao
            {
                PlayerKey = PlayerKey,
                TeamKey = TeamKey,
                PlayerCode = PlayerCode,
                PlayerName = PlayerName
            };
        }

        private void Child_Insert(
            Team parent
            )
        {
            // Insert values into persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IPlayerDal dal = dm.GetProvider<IPlayerDal>();

                using (BypassPropertyChecks)
                {
                    TeamKey = parent.TeamKey;
                    PlayerDao dao = CreateDao();
                    dal.Insert(dao);

                    // Set new data.
                    PlayerKey = dao.PlayerKey;
                }
                //FieldManager.UpdateChildren(this);
            }
        }

        private void Child_Update(
            Team parent
            )
        {
            // Update values in persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IPlayerDal dal = dm.GetProvider<IPlayerDal>();

                using (BypassPropertyChecks)
                {
                    PlayerDao dao = CreateDao();
                    dal.Update(dao);

                    // Set new data.
                }
                //FieldManager.UpdateChildren(this);
            }
        }

        private void Child_DeleteSelf(
            Team parent
            )
        {
            // TODO: delete values
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IPlayerDal dal = dm.GetProvider<IPlayerDal>();

                //Items.Clear();
                //FieldManager.UpdateChildren(this);

                PlayerCriteria criteria = new PlayerCriteria(PlayerKey.Value);
                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
