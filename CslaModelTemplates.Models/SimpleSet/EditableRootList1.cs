using Csla;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.SimpleSet
{
    [Serializable]
    public class EditableRootList1 :
      BusinessListBase<EditableRootList1, EditableChild>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(EditableRootList1), "Role");
        }

        #endregion

        #region Factory Methods

        public static EditableRootList1 NewEditableRootList()
        {
            return DataPortal.Create<EditableRootList1>();
        }

        public static EditableRootList1 GetEditableRootList(int id)
        {
            return DataPortal.Fetch<EditableRootList1>(id);
        }

        private EditableRootList1()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(int criteria)
        {
            RaiseListChangedEvents = false;
            // TODO: load values into memory
            object childData = null;
            foreach (var item in (List<object>)childData)
                this.Add(EditableChild.GetEditableChild(childData));
            RaiseListChangedEvents = true;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // TODO: open database, update values
            //base.Child_Update();
        }

        #endregion
    }
}
