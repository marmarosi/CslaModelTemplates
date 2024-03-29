using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.ComplexList;
using CslaModelTemplates.CslaExtensions.Models;
using System;

namespace CslaModelTemplates.Models.ComplexList
{
    /// <summary>
    /// Represents an item in a read-only player collection.
    /// </summary>
    [Serializable]
    public class PlayerListItem : ReadOnlyModel<PlayerListItem>
    {
        #region Properties

        public static readonly PropertyInfo<string> PlayerIdProperty = RegisterProperty<string>(c => c.PlayerId, RelationshipTypes.PrivateField);
        private long? PlayerKey = null;
        public string PlayerId
        {
            get { return GetProperty(PlayerIdProperty, KeyHash.Encode(ID.Player, PlayerKey)); }
            private set { PlayerKey = KeyHash.Decode(ID.Player, value); }
        }

        public static readonly PropertyInfo<string> PlayerCodeProperty = RegisterProperty<string>(c => c.PlayerCode);
        public string PlayerCode
        {
            get { return GetProperty(PlayerCodeProperty); }
            private set { LoadProperty(PlayerCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> PlayerNameProperty = RegisterProperty<string>(c => c.PlayerName);
        public string PlayerName
        {
            get { return GetProperty(PlayerNameProperty); }
            private set { LoadProperty(PlayerNameProperty, value); }
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.ReadProperty, PlayerNameProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PlayerListItem),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private PlayerListItem()
        { /* require use of factory methods */ }

        internal static PlayerListItem Get(
            PlayerListItemDao dao
            )
        {
            return DataPortal.FetchChild<PlayerListItem>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            PlayerListItemDao dao
            )
        {
            // Set values from data access object.
            PlayerKey = dao.PlayerKey;
            PlayerCode = dao.PlayerCode;
            PlayerName = dao.PlayerName;
        }

        #endregion
    }
}
