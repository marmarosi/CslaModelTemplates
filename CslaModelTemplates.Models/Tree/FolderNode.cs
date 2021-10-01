using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.Tree;
using CslaModelTemplates.CslaExtensions.Models;
using System;

namespace CslaModelTemplates.Models.Tree
{
    /// <summary>
    /// Represents an item in a read-only folder tree.
    /// </summary>
    [Serializable]
    public class FolderNode : ReadOnlyModel<FolderNode>
    {
        #region Properties

        public static readonly PropertyInfo<string> FolderIdProperty = RegisterProperty<string>(c => c.FolderId, RelationshipTypes.PrivateField);
        private long? FolderKey = null;
        public string FolderId
        {
            get { return GetProperty(FolderIdProperty, KeyHash.Encode(ID.Folder, FolderKey)); }
            private set { FolderKey = KeyHash.Decode(ID.Folder, value); }
        }

        public static readonly PropertyInfo<string> ParentIdProperty = RegisterProperty<string>(c => c.ParentId, RelationshipTypes.PrivateField);
        private long? ParentKey = null;
        public string ParentId
        {
            get { return GetProperty(ParentIdProperty, KeyHash.Encode(ID.Folder, ParentKey)); }
            private set { ParentKey = KeyHash.Decode(ID.Folder, value); }
        }

        public static readonly PropertyInfo<string> FolderNameProperty = RegisterProperty<string>(c => c.FolderName);
        public string FolderName
        {
            get { return GetProperty(FolderNameProperty); }
            private set { LoadProperty(FolderNameProperty, value); }
        }

        public static readonly PropertyInfo<int?> LevelProperty = RegisterProperty<int?>(c => c.Level);
        public int? Level
        {
            get { return GetProperty(LevelProperty); }
            private set { LoadProperty(LevelProperty, value); }
        }

        public static readonly PropertyInfo<FolderNodeList> ChildrenProperty = RegisterProperty<FolderNodeList>(c => c.Children);
        public FolderNodeList Children
        {
            get { return GetProperty(ChildrenProperty); }
            private set { LoadProperty(ChildrenProperty, value); }
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.ReadProperty, FolderNameProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(FolderNode),
        //        new IsInRole(AuthorizationActions.GetObject, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private FolderNode()
        { /* require use of factory methods */ }

        internal static FolderNode Fetch(FolderNodeDao dao)
        {
            return DataPortal.FetchChild<FolderNode>(dao);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(
            FolderNodeDao dao
            )
        {
            // Set values from data access object.
            FolderKey = dao.FolderKey;
            ParentKey = dao.ParentKey;
            FolderName = dao.FolderName;
            Level = dao.Level;
            Children = FolderNodeList.Get(dao.Children);
        }

        #endregion
    }
}
