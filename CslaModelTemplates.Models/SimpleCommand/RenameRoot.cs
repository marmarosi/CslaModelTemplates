using Csla;
using Csla.Security;
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
        #region Business Methods

        public static readonly PropertyInfo<long?> RootKeyProperty = RegisterProperty<long?>(c => c.RootKey);
        public long? RootKey
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

        private bool CanExecuteCommand()
        {
            return true;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Renames the specified root.
        /// </summary>
        /// <param name="dto">The data transfer object of the command.</param>
        /// <returns>True when the rename succeeded; otherwise false.</returns>
        public static async Task<bool> Execute(
            RenameRootDto dto
            )
        {
            if (dto.RootKey == null || dto.RootKey == 0)
                throw new CommandException(ValidationText.RenameRoot_RootKey_Required);
            if (string.IsNullOrEmpty(dto.RootName))
                throw new CommandException(ValidationText.RenameRoot_RootName_Required);

            RenameRoot command = new RenameRoot();
            command.RootKey = dto.RootKey;
            command.RootName = dto.RootName;
            command.Result = false;

            if (!command.CanExecuteCommand())
                throw new SecurityException(ValidationText.RenameRoot_Security_Failed);

            command = await Task.Run(() => DataPortal.ExecuteAsync(command));
            return command.Result;
        }

        private RenameRoot()
        { /* require use of factory methods */ }

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
