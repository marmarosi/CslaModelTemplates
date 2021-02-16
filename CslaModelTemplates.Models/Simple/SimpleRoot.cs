using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.Simple
{
    /// <summary>
    /// Represents an editable root object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "SimpleRoot")]
    public class SimpleRoot : EditableModel<SimpleRoot>
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
        //        typeof(SimpleRoot),
        //        new IsInRole(AuthorizationActions.EditObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        #endregion

        #region Factory Methods

        private SimpleRoot()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates a new editable root instance.
        /// </summary>
        /// <returns>The new editable root instance.</returns>
        public static async Task<SimpleRoot> Create()
        {
            return await DataPortal.CreateAsync<SimpleRoot>();
        }

        /// <summary>
        /// Gets an existing editable root instance.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        /// <returns>The requested editable root instance.</returns>
        public static async Task<SimpleRoot> Get(
            SimpleRootCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<SimpleRoot>(criteria);
        }

        /// <summary>
        /// Deletes an existing root.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        public static async void Delete(
            SimpleRootCriteria criteria
            )
        {
            await DataPortal.DeleteAsync<SimpleRoot>(criteria);
        }

        /// <summary>
        /// Rebuilds an editable root instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable root instance.</returns>
        public static async Task<SimpleRoot> FromDto(
            SimpleRootDto dto
            )
        {
            SimpleRoot root = dto.RootKey.HasValue ?
                await DataPortal.FetchAsync<SimpleRoot>(new SimpleRootCriteria()
                {
                    RootKey = dto.RootKey.Value
                }) :
                await DataPortal.CreateAsync<SimpleRoot>();

            //root.RootKey = dto.RootKey;
            root.RootCode = dto.RootCode;
            root.RootName = dto.RootName;
            //root.Timestamp = dto.Timestamp;

            return root;
        }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // Load default values.
            // Omit this override if you have no defaults to set.
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(
            SimpleRootCriteria criteria
            )
        {
            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleRootDal dal = dm.GetProvider<ISimpleRootDal>();
                SimpleRootDao dao = dal.Fetch(criteria);

                using (BypassPropertyChecks)
                {
                    RootKey = dao.RootKey;
                    RootCode = dao.RootCode;
                    RootName = dao.RootName;
                    Timestamp = dao.Timestamp;
                }
            }
        }

        private SimpleRootDao CreateDao()
        {
            // Build the data access object.
            return new SimpleRootDao
            {
                RootKey = RootKey,
                RootCode = RootCode,
                RootName = RootName,
                Timestamp = Timestamp
            };
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            // Insert values into persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleRootDal dal = dm.GetProvider<ISimpleRootDal>();

                using (BypassPropertyChecks)
                {
                    SimpleRootDao dao = CreateDao();
                    dal.Insert(dao);

                    // Set new data.
                    RootKey = dao.RootKey;
                    Timestamp = dao.Timestamp;
                }
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            // Update values in persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleRootDal dal = dm.GetProvider<ISimpleRootDal>();

                using (BypassPropertyChecks)
                {
                    SimpleRootDao dao = CreateDao();
                    dal.Update(dao);

                    // Set new data.
                    Timestamp = dao.Timestamp;
                }
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            if (RootKey.HasValue)
                using (BypassPropertyChecks)
                    DataPortal_Delete(new SimpleRootCriteria(RootKey.Value));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SimpleRootCriteria criteria)
        {
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                ISimpleRootDal dal = dm.GetProvider<ISimpleRootDal>();

                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
