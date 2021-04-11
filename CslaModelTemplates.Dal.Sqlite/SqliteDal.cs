using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Dal.Sqlite
{
    /// <summary>
    /// Represents the data access layer.
    /// </summary>
    public class SqliteDal : DalBase
    {
        protected new SqliteContext DbContext
        {
            get { return base.DbContext as SqliteContext; }
        }
    }
}
