﻿using Csla;
using Csla.Security;
using CslaModelTemplates.Contracts.ComplexCommand;
using CslaModelTemplates.Dal;
using CslaModelTemplates.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.Models.Command
{
    /// <summary>
    /// Counts the roots grouped by the number of their items.
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

        #endregion

        #region Business Rules

        public bool CanExecuteCommand()
        {
            return true;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Counts the roots grouped by the number of their items.
        /// </summary>
        /// <param name="criteria">The criteria of the command.</param>
        /// <returns>The count list.</returns>
        public static async Task<CountRootsList> Execute(
            CountRootsCriteria criteria
            )
        {
            CountRoots command = new CountRoots();
            command.RootName = criteria.RootName;
            command.Result = null;

            if (!command.CanExecuteCommand())
                throw new SecurityException(ValidationText.CountRoots_Security_Failed);

            command = await Task.Run(() => DataPortal.ExecuteAsync(command));
            return command.Result;
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
