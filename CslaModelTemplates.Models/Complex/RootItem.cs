using Csla;
using Csla.Core;
using Csla.Rules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.Complex;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Linq;

namespace CslaModelTemplates.Models.Complex
{
    /// <summary>
    /// Represents an editable root item object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "RootItem")]
    public class RootItem : EditableModel<RootItem>
    {
        #region Business Methods

        public static readonly PropertyInfo<long?> RootItemKeyProperty = RegisterProperty<long?>(c => c.RootItemKey);
        public long? RootItemKey
        {
            get { return GetProperty(RootItemKeyProperty); }
            private set { LoadProperty(RootItemKeyProperty, value); }
        }

        public static readonly PropertyInfo<long?> RootKeyProperty = RegisterProperty<long?>(c => c.RootKey);
        public long? RootKey
        {
            get { return GetProperty(RootKeyProperty); }
            private set { LoadProperty(RootKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> RootItemCodeProperty = RegisterProperty<string>(c => c.RootItemCode);
        [Required]
        [MaxLength(10)]
        public string RootItemCode
        {
            get { return GetProperty(RootItemCodeProperty); }
            set { SetProperty(RootItemCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> RootItemNameProperty = RegisterProperty<string>(c => c.RootItemName);
        [Required]
        [MaxLength(100)]
        public string RootItemName
        {
            get { return GetProperty(RootItemNameProperty); }
            set { SetProperty(RootItemNameProperty, value); }
        }

        /// <summary>
        /// Updates an editable root item from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer objects.</param>
        internal void Update(
            RootItemDto dto
            )
        {
            //RootItemKey = dto.RootItemKey;
            //RootKey = dto.RootKey;
            RootItemCode = dto.RootItemCode;
            RootItemName = dto.RootItemName;
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //BusinessRules.AddRule(new Rule(), IdProperty);
            BusinessRules.AddRule(new UniqueRootItemCodes(RootItemCodeProperty));
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        private class UniqueRootItemCodes : BusinessRule
        {
            // TODO: Add additional parameters to your rule to the constructor
            public UniqueRootItemCodes(
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
                RootItem target = (RootItem)context.Target;
                if (target.Parent == null)
                    return;

                Root root = (Root)target.Parent.Parent;
                var count = root.Items.Count(item => item.RootItemCode == target.RootItemCode);
                if (count > 1)
                    context.AddErrorResult(ValidationText.RootItem_RootItemCode_NotUnique);
            }
        }


        #endregion

        #region Factory Methods

        private RootItem()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Rebuilds an editable root item instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable root item instance.</returns>
        internal static RootItem FromDto(
            RootItemDto dto
            )
        {
            RootItem item = DataPortal.CreateChild<RootItem>();

            //item.RootItemKey = dto.RootItemKey;
            //item.RootKey = dto.RootKey;
            item.RootItemCode = dto.RootItemCode;
            item.RootItemName = dto.RootItemName;

            return item;
        }

        #endregion

        #region Data Access

        //protected override void Child_Create()
        //{
        //    // TODO: load default values
        //    // omit this override if you have no defaults to set
        //    base.Child_Create();
        //}

        private void Child_Fetch(
            RootItemDao dao
            )
        {
            using (BypassPropertyChecks)
            {
                // Set values from data access object.
                RootItemKey = dao.RootItemKey;
                RootKey = dao.RootKey;
                RootItemCode = dao.RootItemCode;
                RootItemName = dao.RootItemName;
            }
        }

        private RootItemDao CreateDao()
        {
            // Build the data access object.
            return new RootItemDao
            {
                RootItemKey = RootItemKey,
                RootKey = RootKey,
                RootItemCode = RootItemCode,
                RootItemName = RootItemName
            };
        }

        private void Child_Insert(
            Root parent
            )
        {
            // Insert values into persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootItemDal dal = dm.GetProvider<IRootItemDal>();

                using (BypassPropertyChecks)
                {
                    RootKey = parent.RootKey;
                    RootItemDao dao = CreateDao();
                    dal.Insert(dao);

                    // Set new data.
                    RootItemKey = dao.RootItemKey;
                }
                //FieldManager.UpdateChildren(this);
            }
        }

        private void Child_Update(
            Root parent
            )
        {
            // Update values in persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootItemDal dal = dm.GetProvider<IRootItemDal>();

                using (BypassPropertyChecks)
                {
                    RootItemDao dao = CreateDao();
                    dal.Update(dao);

                    // Set new data.
                }
                //FieldManager.UpdateChildren(this);
            }
        }

        private void Child_DeleteSelf(
            Root parent
            )
        {
            // TODO: delete values
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootItemDal dal = dm.GetProvider<IRootItemDal>();

                //Items.Clear();
                //FieldManager.UpdateChildren(this);

                RootItemCriteria criteria = new RootItemCriteria(RootItemKey.Value);
                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
