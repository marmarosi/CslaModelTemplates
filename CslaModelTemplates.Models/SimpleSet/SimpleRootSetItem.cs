using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.SimpleSet;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SimpleSet
{
    /// <summary>
    /// Represents an editable child object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "SimpleRootSetItem")]
    public class SimpleRootSetItem : EditableModel<SimpleRootSetItem>
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
        //        typeof(SimpleRootSetItem),
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
        internal static SimpleRootSetItem Get(
            SimpleRootSetItemDao dao
            )
        {
            return DataPortal.FetchChild<SimpleRootSetItem>(dao);
        }

        /// <summary>
        /// Updates an editable root from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer objects.</param>
        internal void Update(
            SimpleRootSetItemDto dto
            )
        {
            //RootKey = dto.RootKey;
            RootCode = dto.RootCode;
            RootName = dto.RootName;
            //Timestamp = dto.Timestamp;
        }

        #endregion

        #region Factory Methods

        private SimpleRootSetItem()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates an editable root instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The new editable root instance.</returns>
        internal static async Task<SimpleRootSetItem> Create(
            SimpleRootSetItemDto dto
            )
        {
            SimpleRootSetItem root = null;
            await Task.Run(() => root = DataPortal.CreateChild<SimpleRootSetItem>());
            root.Update(dto);
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
            SimpleRootSetItemDao dao
            )
        {
            using (BypassPropertyChecks)
            {
                // Set values from data access object.
                RootKey = dao.RootKey;
                RootCode = dao.RootCode;
                RootName = dao.RootName;
                Timestamp = dao.Timestamp;
            }
        }

        private SimpleRootSetItemDao CreateDao()
        {
            // Build the data access object.
            return new SimpleRootSetItemDao
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
                ISimpleRootSetItemDal dal = dm.GetProvider<ISimpleRootSetItemDal>();

                using (BypassPropertyChecks)
                {
                    SimpleRootSetItemDao dao = CreateDao();
                    dal.Insert(dao);

                    // Set new data.
                    RootKey = dao.RootKey;
                    Timestamp = dao.Timestamp;
                }
            }
        }

        private void Child_Update()
        {
            // Update values in persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleRootSetItemDal dal = dm.GetProvider<ISimpleRootSetItemDal>();

                using (BypassPropertyChecks)
                {
                    SimpleRootSetItemDao dao = CreateDao();
                    dal.Update(dao);

                    // Set new data.
                    Timestamp = dao.Timestamp;
                }
            }
        }

        private void Child_DeleteSelf()
        {
            // TODO: delete values
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleRootSetItemDal dal = dm.GetProvider<ISimpleRootSetItemDal>();

                SimpleRootSetItemCriteria criteria = new SimpleRootSetItemCriteria(RootKey.Value);
                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
