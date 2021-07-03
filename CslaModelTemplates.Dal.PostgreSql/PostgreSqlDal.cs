using CslaModelTemplates.Dal;

namespace CslaModelTemplates.Dal.PostgreSql
{
    /// <summary>
    /// Represents the data access layer.
    /// </summary>
    public class PostgreSqlDal : DalBase
    {
        protected new PostgreSqlContext DbContext
        {
            get { return base.DbContext as PostgreSqlContext; }
        }
    }
}
