using Csla;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.ComplexSet;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;

namespace CslaModelTemplates.Models.ComplexSet
{
    /// <summary>
    /// Represents an editable child object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "RootSetItem")]
    public class RootSetItem : EditableModel<RootSetItem>
    {
        #region Business Methods

        public static readonly PropertyInfo<long?> RootKeyProperty = RegisterProperty<long?>(c => c.RootKey);
        public long? RootKey
        {
            get { return GetProperty(RootKeyProperty); }
            private set { LoadProperty(RootKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> RootCodeProperty = RegisterProperty<string>(c => c.RootCode);
        [Required]
        [MaxLength(10)]
        public string RootCode
        {
            get { return GetProperty(RootCodeProperty); }
            set { SetProperty(RootCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> RootNameProperty = RegisterProperty<string>(c => c.RootName);
        [Required]
        [MaxLength(100)]
        public string RootName
        {
            get { return GetProperty(RootNameProperty); }
            set { SetProperty(RootNameProperty, value); }
        }

        public static readonly PropertyInfo<RootSetRootItems> ItemsProperty = RegisterProperty<RootSetRootItems>(c => c.Items);
        public RootSetRootItems Items
        {
            get { return GetProperty(ItemsProperty); }
            private set { LoadProperty(ItemsProperty, value); }
        }

        public static readonly PropertyInfo<DateTime?> TimestampProperty = RegisterProperty<DateTime?>(c => c.Timestamp);
        public DateTime? Timestamp
        {
            get { return GetProperty(TimestampProperty); }
            private set { LoadProperty(TimestampProperty, value); }
        }

        /// <summary>
        /// Updates an editable root from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer objects.</param>
        internal void Update(
            RootSetItemDto dto
            )
        {
            //RootKey = dto.RootKey;
            RootCode = dto.RootCode;
            RootName = dto.RootName;
            Items.Update(dto.Items);
            //Timestamp = dto.Timestamp;
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //BusinessRules.AddRule(new Rule(), IdProperty);
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        internal static RootSetItem Get(
            RootSetItemDao dao
            )
        {
            return DataPortal.FetchChild<RootSetItem>(dao);
        }

        private RootSetItem()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Rebuilds an editable root item instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable root item instance.</returns>
        internal static RootSetItem FromDto(
            RootSetItemDto dto
            )
        {
            RootSetItem root = dto.RootKey.HasValue ?
                DataPortal.FetchChild<RootSetItem>(dto.ToDao()) :
                DataPortal.CreateChild<RootSetItem>();

            //root.RootKey = dto.RootKey;
            root.RootCode = dto.RootCode;
            root.RootName = dto.RootName;
            root.Items.Update(dto.Items);
            //root.Timestamp = dto.Timestamp;

            return root;
        }

        #endregion

        #region Data Access

        protected override void Child_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            //base.Child_Create();
        }

        private void Child_Fetch(
            RootSetItemDao dao
            )
        {
            using (BypassPropertyChecks)
            {
                // Set values from data access object.
                RootKey = dao.RootKey;
                RootCode = dao.RootCode;
                RootName = dao.RootName;
                Items = DataPortal.FetchChild<RootSetRootItems>(dao.Items);
                Timestamp = dao.Timestamp;
            }
        }

        private RootSetItemDao CreateDao()
        {
            // Build the data access object.
            return new RootSetItemDao
            {
                RootKey = RootKey,
                RootCode = RootCode,
                RootName = RootName,
                Timestamp = Timestamp
            };
        }

        private void Child_Insert()
        {
            // Insert values into persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootSetItemDal dal = dm.GetProvider<IRootSetItemDal>();

                using (BypassPropertyChecks)
                {
                    RootSetItemDao dao = CreateDao();
                    dal.Insert(dao);

                    // Set new data.
                    RootKey = dao.RootKey;
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
                IRootSetItemDal dal = dm.GetProvider<IRootSetItemDal>();

                using (BypassPropertyChecks)
                {
                    RootSetItemDao dao = CreateDao();
                    dal.Update(dao);

                    // Set new data.
                    Timestamp = dao.Timestamp;
                }
                FieldManager.UpdateChildren(this);
            }
        }

        private void Child_DeleteSelf()
        {
            // TODO: delete values
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootSetItemDal dal = dm.GetProvider<IRootSetItemDal>();

                Items.Clear();
                FieldManager.UpdateChildren(this);

                RootSetItemCriteria criteria = new RootSetItemCriteria(RootKey.Value);
                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
