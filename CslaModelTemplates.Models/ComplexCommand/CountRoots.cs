using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using CslaModelTemplates.Common.Validations;
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
        #region Properties

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

        //private void Validate()
        //{
        //    if (string.IsNullOrEmpty(RootName))
        //        throw new CommandException(ValidationText.RenameRoot_RootName_Required);
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountRoots),
        //        new IsInRole(AuthorizationActions.ExecuteMethod, "Manager")
        //        );
        //}

        #endregion

        #region Factory Methods

        private CountRoots()
        { /* require use of factory methods */ }

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

            //command.Validate();

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
