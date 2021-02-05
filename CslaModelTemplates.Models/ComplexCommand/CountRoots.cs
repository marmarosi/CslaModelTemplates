using Csla;
using CslaModelTemplates.Contracts.ComplexCommand;
using CslaModelTemplates.Dal;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Models.Command
{
    /// <summary>
    /// Counts the roots frouped by the number of their items.
    /// </summary>
    [Serializable]
    public class CountRoots : CommandBase<CountRoots>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> RootNameProperty = RegisterProperty<string>(c => c.RootName);
        public string RootName
        {
            get { return ReadProperty(RootNameProperty); }
            private set { LoadProperty(RootNameProperty, value); }
        }

        public static readonly PropertyInfo<CountRootsList> ResultProperty = RegisterProperty<CountRootsList>(c => c.Result);
        public CountRootsList Result
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
        /// Creates a new count roots by item count command.
        /// </summary>
        /// <param name="criteria">The criteria of the command.</param>
        /// <returns>The new count roots by item count command.</returns>
        public static CountRoots Create(
            CountRootsCriteria criteria
            )
        {
            CountRoots cmd = new CountRoots();
            cmd.RootName = criteria.RootName;
            cmd.Result = null;
            return cmd;
        }

        private CountRoots()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        protected override void DataPortal_Execute()
        {
            // Execute the command.
            using (var ctx = DalFactory.GetManager())
            {
                ICountRootsDal dal = ctx.GetProvider<ICountRootsDal>();
                CountRootsCriteria criteria = new CountRootsCriteria(RootName);
                List<CountRootsListItemDao> list = dal.Execute(criteria);

                // Set new data.
                Result = CountRootsList.Get(list);
            }
        }

        #endregion
    }
}
