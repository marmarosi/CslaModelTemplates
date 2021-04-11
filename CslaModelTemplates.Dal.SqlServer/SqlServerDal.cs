using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Dal.SqlServer
{
    /// <summary>
    /// Represents the data access layer.
    /// </summary>
    public class SqlServerDal : DalBase
    {
        protected new SqlServerContext DbContext
        {
            get { return base.DbContext as SqlServerContext; }
        }
    }
}
