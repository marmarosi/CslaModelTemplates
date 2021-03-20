using Csla;
using Csla.Core;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Models;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.LookUp;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.LookUp
{
    /// <summary>
    /// Represents an editable member object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ValidationText), ObjectName = "Member")]
    public class Member : EditableModel<Member>
    {
        #region Properties

        public static readonly PropertyInfo<long?> PersonKeyProperty = RegisterProperty<long?>(c => c.PersonKey);
        public long? PersonKey
        {
            get { return GetProperty(PersonKeyProperty); }
            private set { LoadProperty(PersonKeyProperty, value); }
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
            // Add validation rules.
            BusinessRules.AddRule(new UniquePersonKeys(PersonKeyProperty));

            // Add authorization rules.
            //BusinessRules.AddRule(new IsInRole(
            //    AuthorizationActions.WriteProperty, PlayerCodeProperty, "Manager"));
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(Player),
        //        new IsInRole(AuthorizationActions.EditObject, "Manager")
        //        );
        //}

        private class UniquePersonKeys : BusinessRule
        {
            // TODO: Add additional parameters to your rule to the constructor
            public UniquePersonKeys(
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

            protected override void Execute(
                IRuleContext context
                )
            {
                // TODO: Add actual rule code here. 
                //if (broken condition)
                //{
                //  context.AddErrorResult("Broken rule message");
                //}
                Member target = (Member)context.Target;
                if (target.Parent == null)
                    return;

                Group group = (Group)target.Parent.Parent;
                var count = group.Members.Count(member => member.PersonKey == target.PersonKey);
                if (count > 1)
                    context.AddErrorResult(ValidationText.Member_PersonKey_NotUnique);
            }
        }

        #endregion

        #region Business Methods

        /// <summary>
        /// Creates an editable member instance from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>The new editable member instance.</returns>
        internal static async Task<Member> Create(
            MemberDto dto
            )
        {
            Member member = null;
            member = await Task.Run(() => DataPortal.CreateChild<Member>());
            member.Update(dto);
            return member;
        }

        /// <summary>
        /// Updates an editable member from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer objects.</param>
        internal void Update(
            MemberDto dto
            )
        {
            PersonKey = dto.PersonKey;
            PersonName = dto.PersonName;
        }

        #endregion

        #region Factory Methods

        private Member()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        //protected override void Child_Create()
        //{
        //    // TODO: load default values
        //    // omit this override if you have no defaults to set
        //}

        private void Child_Fetch(
            MemberDao dao
            )
        {
            using (BypassPropertyChecks)
            {
                // Set values from data access object.
                PersonKey = dao.PersonKey;
                PersonName = dao.PersonName;
            }
        }

        private MemberDao CreateDao(
            long? groupKey
            )
        {
            // Build the data access object.
            return new MemberDao
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
                IMemberDal dal = dm.GetProvider<IMemberDal>();

                using (BypassPropertyChecks)
                {
                    MemberDao dao = CreateDao(parent.GroupKey);
                    dal.Insert(dao);

                    // Set new data.
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
            // TODO: delete values
            // Delete values from persistent storage.
            using (IDalManager dm = DalFactory.GetManager())
            {
                IMemberDal dal = dm.GetProvider<IMemberDal>();

                //Items.Clear();
                //FieldManager.UpdateChildren(this);

                MemberDao dao = CreateDao(parent.GroupKey);
                dal.Delete(dao);
            }
        }

        #endregion
    }
}
