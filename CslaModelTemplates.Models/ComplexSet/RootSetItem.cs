using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.ComplexSet;
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
    [ValidationResourceType(typeof(ValidationText), ObjectName = "RootSetItem")]
    public class RootSetItem : EditableModel<RootSetItem>
    {
        #region Properties

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

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add validation rules.
        //    BusinessRules.AddRule(new Required(RootNameProperty));

        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.WriteProperty, RootNameProperty, "Manager"));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(RootSetItem),
        //        new IsInRole(AuthorizationActions.EditObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Gets an existing editable root instance.
        /// </summary>
        /// <param name="dao">The data access objects.</param>
        /// <returns>The requested editable root instance.</returns>
        internal static RootSetItem Get(
            RootSetItemDao dao
            )
        {
            return DataPortal.FetchChild<RootSetItem>(dao);
        }

        /// <summary>
        /// Updates an editable root from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer objects.</param>
        internal async Task Update(
            RootSetItemDto dto
            )
        {
            //RootKey = dto.RootKey;
            RootCode = dto.RootCode;
            RootName = dto.RootName;
            await Items.Update(dto.Items);
            //Timestamp = dto.Timestamp;
        }

        #endregion

        #region Factory Methods

        private RootSetItem()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates an editable root instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The new editable root instance.</returns>
        internal static async Task<RootSetItem> Create(
            RootSetItemDto dto
            )
        {
            RootSetItem root = null;
            await Task.Run(() => root = DataPortal.CreateChild<RootSetItem>());

            //root.RootKey = dto.RootKey;
            root.RootCode = dto.RootCode;
            root.RootName = dto.RootName;
            await root.Items.Update(dto.Items);
            //root.Timestamp = dto.Timestamp;

            return root;
        }

        #endregion

        #region Data Access

        //protected override void Child_Create()
        //{
        //    // TODO: load default values
        //    // omit this override if you have no defaults to set
        //}

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
