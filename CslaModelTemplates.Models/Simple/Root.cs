using Csla;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.Simple;
using CslaModelTemplates.Resources;
using System;

namespace CslaModelTemplates.Models.Simple
{
    /// <summary>
    /// Represents an editable root object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "Root")]
    public class Root : EditableModel<Root>
    {
        #region Business Methods

        public static readonly PropertyInfo<long?> RootKeyProperty = RegisterProperty<long?>(c => c.RootKey);
        public long? RootKey
        {
            get { return GetProperty(RootKeyProperty); }
            set { SetProperty(RootKeyProperty, value); }
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
        /// Gets the data transfer object of the root object.
        /// </summary>
        /// <returns>The data transfer object of the root object.</returns>
        public RootDto AsDto()
        {
            return new RootDto
            {
                RootKey = RootKey,
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
        /// Creates a new root instance.
        /// </summary>
        /// <returns>The new root instance.</returns>
        public static Root New()
        {
            return DataPortal.Create<Root>();
        }

        /// <summary>
        /// Gets an existing root instance.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        /// <returns>The requested root instance.</returns>
        public static Root Get(
            RootCriteria criteria
            )
        {
            return DataPortal.Fetch<Root>(criteria);
        }

        /// <summary>
        /// Deletes an existing root instance.
        /// </summary>
        /// <param name="criteria">The criteria of the root.</param>
        public static void Delete(
            RootCriteria criteria
            )
        {
            DataPortal.Delete<Root>(criteria);
        }

        private Root()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Rebuilds a root instance and sets the new property values.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt root instance.</returns>
        public static Root From(
            RootDto dto
            )
        {
            Root root = dto.RootKey.HasValue ?
                DataPortal.Fetch<Root>(new RootCriteria()
                {
                    RootKey = dto.RootKey.Value
                }) :
                New();

            //root.RootKey = dto.RootKey;
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
            RootCriteria criteria
            )
        {
            // Load values.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootDal dal = dm.GetProvider<IRootDal>();
                RootDao dao = dal.Fetch(criteria);

                using (BypassPropertyChecks)
                {
                    RootKey = dao.RootKey;
                    RootName = dao.RootName;
                    Timestamp = dao.Timestamp;
                }
            }
        }

        private RootDao CreateDao()
        {
            return new RootDao
            {
                RootKey = RootKey,
                RootName = RootName,
                Timestamp = Timestamp
            };
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            // Insert values.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootDal dal = dm.GetProvider<IRootDal>();

                using (BypassPropertyChecks)
                {
                    RootDao dao = CreateDao();
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
            // Update values.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootDal dal = dm.GetProvider<IRootDal>();

                using (BypassPropertyChecks)
                {
                    RootDao dao = CreateDao();
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
                        new RootCriteria()
                        {
                            RootKey = RootKey.Value
                        });
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(RootCriteria criteria)
        {
            // Delete values.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IRootDal dal = dm.GetProvider<IRootDal>();

                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
