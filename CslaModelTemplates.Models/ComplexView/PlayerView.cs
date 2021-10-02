using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.ComplexView;
using CslaModelTemplates.CslaExtensions.Models;
using System;

namespace CslaModelTemplates.Models.ComplexView
{
    /// <summary>
    /// Represents an item in a read-only player collection.
    /// </summary>
    [Serializable]
    public class PlayerView : ReadOnlyModel<PlayerView>
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
        //        typeof(PlayerView),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private PlayerView()
        { /* require use of factory methods */ }

        internal static PlayerView Get(
            PlayerViewDao dao
            )
        {
            return DataPortal.FetchChild<PlayerView>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            PlayerViewDao dao
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
