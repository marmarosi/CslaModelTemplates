using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.SimpleCommand;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.SimpleCommand
{
    /// <summary>
    /// Renames the specified root.
    /// </summary>
    [Serializable]
    public class RenameRoot : CommandBase<RenameRoot>
    {
        #region Properties

        public static readonly PropertyInfo<long> RootKeyProperty = RegisterProperty<long>(c => c.RootKey);
        public long RootKey
        {
            get { return ReadProperty(RootKeyProperty); }
            private set { LoadProperty(RootKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> RootNameProperty = RegisterProperty<string>(c => c.RootName);
        public string RootName
        {
            get { return ReadProperty(RootNameProperty); }
            private set { LoadProperty(RootNameProperty, value); }
        }

        public static readonly PropertyInfo<bool> ResultProperty = RegisterProperty<bool>(c => c.Result);
        public bool Result
        {
            get { return ReadProperty(ResultProperty); }
            private set { LoadProperty(ResultProperty, value); }
        }

        #endregion

        #region Business Rules

        private void Validate()
        {
            if (string.IsNullOrEmpty(RootName))
                throw new CommandException(ValidationText.RenameRoot_RootName_Required);
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(RenameRoot),
        //        new IsInRole(AuthorizationActions.ExecuteMethod, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private RenameRoot()
        { /* require use of factory methods */ }

        /// <summary>
        /// Renames the specified root.
        /// </summary>
        /// <param name="dto">The data transfer object of the command.</param>
        /// <returns>True when the rename succeeded; otherwise false.</returns>
        public static async Task<bool> Execute(
            RenameRootDto dto
            )
        {
            RenameRoot command = new RenameRoot();
            command.RootKey = dto.RootKey;
            command.RootName = dto.RootName;
            command.Result = false;

            command.Validate();

            command = await Task.Run(() => DataPortal.ExecuteAsync(command));
            return command.Result;
        }

        #endregion

        #region Data Access

        protected override void DataPortal_Execute()
        {
            // Execute the command.
            using (var ctx = DalFactory.GetManager())
            {
                IRenameRootDal dal = ctx.GetProvider<IRenameRootDal>();

                RenameRootDao dao = new RenameRootDao(RootKey, RootName);
                dal.Execute(dao);

                // Set new data.
                Result = true;
            }
        }

        #endregion
    }
}
