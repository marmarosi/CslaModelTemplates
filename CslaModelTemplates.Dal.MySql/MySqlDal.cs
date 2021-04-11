using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Dal.MySql
{
    /// <summary>
    /// Represents the data access layer.
    /// </summary>
    public class MySqlDal : DalBase
    {
        protected new MySqlContext DbContext
        {
            get { return base.DbContext as MySqlContext; }
        }
    }
}
