using Csla;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Contracts.SimpleCommand;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;

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

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            //if (!CanExecuteCommand())
            //    throw new SecurityException(SecurityText.Login_Execute);

            if (RootKey == null || RootKey == 0)
                throw new CommandException(ValidationText.RenameRoot_RootKey_Required);
            if (string.IsNullOrEmpty(RootName))
                throw new CommandException(ValidationText.RenameRoot_RootName_Required);

            DataPortal_Execute();
        }

        #endregion

        #region Business Rules

        public static bool CanExecuteCommand()
        {
            return true;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Creates a new rename root command.
        /// </summary>
        /// <param name="dto">The data transfer object of the command.</param>
        /// <returns>The new rename root command.</returns>
        public static RenameRoot Create(
            RenameRootDto dto
            )
        {
            RenameRoot cmd = new RenameRoot();
            cmd.RootKey = dto.RootKey;
            cmd.RootName = dto.RootName;
            cmd.Result = false;
            return cmd;
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
