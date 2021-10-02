using Csla;
using Csla.Core;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Contracts;
using CslaModelTemplates.Contracts.Junction;
using CslaModelTemplates.CslaExtensions.Models;
using CslaModelTemplates.CslaExtensions.Validations;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.Junction
{
    /// <summary>
    /// Represents an editable group-person object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "GroupPerson")]
    public class GroupPerson : EditableModel<GroupPerson>
    {
        #region Properties

        private long? PersonKey
        {
            get { return KeyHash.Decode(ID.Person, PersonId); }
            set { PersonId = KeyHash.Encode(ID.Person, value); }
        }

        public static readonly PropertyInfo<string> PersonIdProperty = RegisterProperty<string>(c => c.PersonId);
        public string PersonId
        {
            get { return GetProperty(PersonIdProperty); }
            private set { SetProperty(PersonIdProperty, value); }
        }

        public static readonly PropertyInfo<string> PersonNameProperty = RegisterProperty<string>(c => c.PersonName);
        public string PersonName
        {
            get { return GetProperty(PersonNameProperty); }
            private set { LoadProperty(PersonNameProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // Call base class implementation to add data annotation rules to BusinessRules.
            // NOTE: DataAnnotation rules is always added with Priority = 0.
            base.AddBusinessRules();

            //// Add validation rules.
            //BusinessRules.AddRule(new Required(PersonNameProperty));
            BusinessRules.AddRule(new UniquePersonIds(PersonIdProperty));

            //// Add authorization rules.
            //BusinessRules.AddRule(new IsInRole(
            //    AuthorizationActions.WriteProperty, PersonNameProperty, "Manager"));
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(GroupPerson),
        //        new IsInRole(AuthorizationActions.EditObject, "Manager")
        //        );
        //}

        private class UniquePersonIds : BusinessRule
        {
            // Add additional parameters to your rule to the constructor.
            public UniquePersonIds(
                IPropertyInfo primaryProperty
                )
              : base(primaryProperty)
            {
                // If you are  going to add InputProperties make sure to
                // uncomment line below as InputProperties is NULL by default.
                //if (InputProperties == null) InputProperties = new List<IPropertyInfo>();

                // Add additional constructor code here.

                // Marke rule for IsAsync if Execute contains asyncronous code IsAsync = true; 
            }

            protected override void Execute(
                IRuleContext context
                )
            {
                GroupPerson target = (GroupPerson)context.Target;
                if (target.Parent == null)
                    return;

                Group group = (Group)target.Parent.Parent;
                var count = group.Persons.Count(gp => gp.PersonId == target.PersonId);
                if (count > 1)
                    context.AddErrorResult(ValidationText.GroupPerson_PersonId_NotUnique);
            }
        }

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable group-person from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer objects.</param>
        internal void Update(
            GroupPersonDto dto
            )
        {
            PersonKey = KeyHash.Decode(ID.Person, dto.PersonId);
            PersonName = dto.PersonName;

            BusinessRules.CheckRules();
        }

        #endregion

        #region Factory Methods

        private GroupPerson()
        { /* Require use of factory methods */ }

        /// <summary>
        /// Creates an editable group-person instance from the data transfer object.
        /// </summary>
        /// <param name="parent">The parent collection.</param>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The new editable group-person instance.</returns>
        internal static async Task<GroupPerson> Create(
            IParent parent,
            GroupPersonDto dto
            )
        {
            GroupPerson groupPerson = await Task.Run(() => DataPortal.CreateChild<GroupPerson>());
            groupPerson.SetParent(parent);
            groupPerson.Update(dto);
            return groupPerson;
        }

        #endregion

        #region Data Access

        //[RunLocal]
        //protected override void Child_Create()
        //{
        //    // Load default values.
        //    // Omit this override if you have no defaults to set.
        //}

        private void Child_Fetch(
            GroupPersonDao dao
            )
        {
            using (BypassPropertyChecks)
            {
                // Set values from data access object.
                PersonKey = dao.PersonKey;
                PersonName = dao.PersonName;
            }
        }

        private GroupPersonDao CreateDao(
            long? groupKey
            )
        {
            // Build the data access object.
            return new GroupPersonDao
            {
                GroupKey = groupKey,
                PersonKey = PersonKey,
                PersonName = PersonName
            };
        }

        private void Child_Insert(
            Group parent
            )
        {
            // Insert values into persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IGroupPersonDal dal = dm.GetProvider<IGroupPersonDal>();

                using (BypassPropertyChecks)
                {
                    GroupPersonDao dao = CreateDao(parent.GroupKey);
                    dal.Insert(dao);

                    // Set new data.
                    PersonName = dao.PersonName;
                }
                //FieldManager.UpdateChildren(this);
            }
        }

        private void Child_Update(
            Group parent
            )
        {
            throw new NotImplementedException();
        }

        private void Child_DeleteSelf(
            Group parent
            )
        {
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IGroupPersonDal dal = dm.GetProvider<IGroupPersonDal>();

                //Items.Clear();
                //FieldManager.UpdateChildren(this);

                GroupPersonDao dao = CreateDao(parent.GroupKey);
                dal.Delete(dao);
            }
        }

        #endregion
    }
}
