using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.CslaExtensions.Validations;
using CslaModelTemplates.Contracts.Junction;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.Junction
{
    /// <summary>
    /// Represents an editable group object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "Group")]
    public class Group : EditableModel<Group>
    {
        #region Properties

        public static readonly PropertyInfo<long?> GroupKeyProperty = RegisterProperty<long?>(c => c.GroupKey);
        public long? GroupKey
        {
            get { return GetProperty(GroupKeyProperty); }
            private set { LoadProperty(GroupKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> GroupCodeProperty = RegisterProperty<string>(c => c.GroupCode);
        [Required]
        [MaxLength(10)]
        public string GroupCode
        {
            get { return GetProperty(GroupCodeProperty); }
            set { SetProperty(GroupCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> GroupNameProperty = RegisterProperty<string>(c => c.GroupName);
        [Required]
        [MaxLength(100)]
        public string GroupName
        {
            get { return GetProperty(GroupNameProperty); }
            set { SetProperty(GroupNameProperty, value); }
        }

        public static readonly PropertyInfo<GroupPersons> GroupPersonsProperty = RegisterProperty<GroupPersons>(c => c.Persons, RelationshipTypes.Child);
        public GroupPersons Persons
        {
            get { return GetProperty(GroupPersonsProperty); }
            private set { LoadProperty(GroupPersonsProperty, value); }
        }

        public static readonly PropertyInfo<DateTime?> TimestampProperty = RegisterProperty<DateTime?>(c => c.Timestamp);
        public DateTime? Timestamp
        {
            get { return GetProperty(TimestampProperty); }
            private set { LoadProperty(TimestampProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // Call base class implementation to add data annotation rules to BusinessRules.
            // NOTE: DataAnnotation rules is always added with Priority = 0.
            base.AddBusinessRules();

            //// Add validation rules.
            //BusinessRules.AddRule(new Required(GroupNameProperty));

            //// Add authorization rules.
            //BusinessRules.AddRule(new IsInRole(
            //    AuthorizationActions.WriteProperty, GroupNameProperty, "Manager"));
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(Group),
        //        new IsInRole(AuthorizationActions.EditObject, "Manager")
        //        );
        //}

        #endregion

        #region Business Methods

        #endregion

        #region Factory Methods

        private Group()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates a new editable group instance.
        /// </summary>
        /// <returns>The new editable group instance.</returns>
        public static async Task<Group> Create()
        {
            return await DataPortal.CreateAsync<Group>();
        }

        /// <summary>
        /// Gets an existing editable group instance.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        /// <returns>The requested editable group instance.</returns>
        public static async Task<Group> Get(
            GroupCriteria criteria
            )
        {
            return await DataPortal.FetchAsync<Group>(criteria);
        }

        /// <summary>
        /// Deletes an existing group.
        /// </summary>
        /// <param name="criteria">The criteria of the group.</param>
        public static async void Delete(
            GroupCriteria criteria
            )
        {
            await DataPortal.DeleteAsync<Group>(criteria);
        }

        /// <summary>
        /// Rebuilds an editable group instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The rebuilt editable group instance.</returns>
        public static async Task<Group> FromDto(
            GroupDto dto
            )
        {
            Group group = dto.GroupKey.HasValue ?
                await DataPortal.FetchAsync<Group>(new GroupCriteria()
                {
                    GroupKey = dto.GroupKey.Value
                }) :
                await DataPortal.CreateAsync<Group>();

            //group.GroupKey = dto.GroupKey;
            group.GroupCode = dto.GroupCode;
            group.GroupName = dto.GroupName;
            await group.Persons.Update(dto.Persons);
            //group.Timestamp = dto.Timestamp;

            group.BusinessRules.CheckRules();
            return group;
        }

        #endregion

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // Load default values.
            // Omit this override if you have no defaults to set.
            Persons = GroupPersons.Create();
        }

        private void DataPortal_Fetch(
            GroupCriteria criteria
            )
        {
            // Load values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IGroupDal dal = dm.GetProvider<IGroupDal>();
                GroupDao dao = dal.Fetch(criteria);

                using (BypassPropertyChecks)
                {
                    GroupKey = dao.GroupKey;
                    GroupCode = dao.GroupCode;
                    GroupName = dao.GroupName;
                    Persons = DataPortal.FetchChild<GroupPersons>(dao.Persons);
                    Timestamp = dao.Timestamp;
                }
            }
        }

        private GroupDao CreateDao()
        {
            // Build the data access object.
            return new GroupDao
            {
                GroupKey = GroupKey,
                GroupCode = GroupCode,
                GroupName = GroupName,
                Timestamp = Timestamp
            };
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            // Insert values into persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IGroupDal dal = dm.GetProvider<IGroupDal>();

                using (BypassPropertyChecks)
                {
                    GroupDao dao = CreateDao();
                    dal.Insert(dao);

                    // Set new data.
                    GroupKey = dao.GroupKey;
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
                IGroupDal dal = dm.GetProvider<IGroupDal>();

                using (BypassPropertyChecks)
                {
                    GroupDao dao = CreateDao();
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
            if (GroupKey.HasValue)
                using (BypassPropertyChecks)
                    DataPortal_Delete(new GroupCriteria(GroupKey.Value));
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(GroupCriteria criteria)
        {
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IGroupDal dal = dm.GetProvider<IGroupDal>();

                if (!GroupKey.HasValue)
                    DataPortal_Fetch(criteria);

                Persons.Clear();
                FieldManager.UpdateChildren(this);

                dal.Delete(criteria);
            }
        }

        #endregion
    }
}
