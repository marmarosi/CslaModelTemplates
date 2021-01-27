using Csla;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.Simple;
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
        #region Business Methods

        public static readonly PropertyInfo<long?> RootKeyProperty = RegisterProperty<long?>(c => c.RootKey);
        public long? RootKey
        {
            get { return GetProperty(RootKeyProperty); }
            set { SetProperty(RootKeyProperty, value); }
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

        /// <summary>
        /// Gets the data transfer object of the editable root object.
        /// </summary>
        /// <returns>The data transfer object of the editable root object.</returns>
        public SimpleRootDto AsDto()
        {
            return new SimpleRootDto
            {
                RootKey = RootKey,
                RootCode = RootCode,
                RootName = RootName,
                Timestamp = Timestamp
            };
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // Add validation rules.
            base.AddBusinessRules();

            //BusinessRules.AddRule(new Rule(IdProperty));
        }

        private static void AddObjectAuthorizationRules()
        {
            // Add authorization rules.
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Creates a new editable root instance.
        /// </summary>
        /// <returns>The new editable root instance.</returns>
        public static SimpleRoot Create()
        {
            return DataPortal.Create<SimpleRoot>();
        }

        /// <summary>
        /// Creates a new editable root instance.
        /// </summary>
        /// <returns>The new editable root instance.</returns>
        public static async Task<SimpleRoot> CreateAsync()
        {
            return await DataPortal.CreateAsync<SimpleRoot>();
        }

        /// <summary>
        /// Gets an existing editable root instance.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        /// <returns>The requested editable root instance.</returns>
        public static SimpleRoot Get(
            SimpleRootCriteria criteria
            )
        {
            return DataPortal.Fetch<SimpleRoot>(criteria);
        }

        /// <summary>
        /// Gets an existing editable root instance.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        /// <returns>The requested editable root instance.</returns>
        public static async Task<SimpleRoot> GetAsync(
            SimpleRootCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<SimpleRoot>(criteria);
        }

        /// <summary>
        /// Deletes an existing root.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        public static void Delete(
            SimpleRootCriteria criteria
            )
        {
            DataPortal.Delete<SimpleRoot>(criteria);
        }

        /// <summary>
        /// Deletes an existing root.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        public static async void DeleteAsync(
            SimpleRootCriteria criteria
            )
        {
            await DataPortal.DeleteAsync<SimpleRoot>(criteria);
        }

        private SimpleRoot()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Rebuilds an editable root instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable root instance.</returns>
        public static SimpleRoot FromDto(
            SimpleRootDto dto
            )
        {
            SimpleRoot root = dto.RootKey.HasValue ?
                DataPortal.Fetch<SimpleRoot>(new SimpleRootCriteria()
                {
                    RootKey = dto.RootKey.Value
                }) :
                Create();

            //root.RootKey = dto.RootKey;
            root.RootCode = dto.RootCode;
            root.RootName = dto.RootName;
            //root.Timestamp = dto.Timestamp;

            return root;
        }

        /// <summary>
        /// Rebuilds an editable root instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable root instance.</returns>
        public static async Task<SimpleRoot> FromDtoAsync(
            SimpleRootDto dto
            )
        {
            SimpleRoot root = dto.RootKey.HasValue ?
                await DataPortal.FetchAsync<SimpleRoot>(new SimpleRootCriteria()
                {
                    RootKey = dto.RootKey.Value
                }) :
                await CreateAsync();

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
                FieldManager.UpdateChildren(this);
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
                FieldManager.UpdateChildren(this);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            if (RootKey.HasValue)
                using (BypassPropertyChecks)
                    DataPortal_Delete(
                        new SimpleRootCriteria()
                        {
                            RootKey = RootKey.Value
                        });
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
