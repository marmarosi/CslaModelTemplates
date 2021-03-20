using CslaModelTemplates.Common;

namespace CslaModelTemplates.Dal.SqlServer
{
    public class SqlServerDal : DalBase
    {
        protected SqlServerContext Db
        {
            get { return DbContext as SqlServerContext; }
        }
    }
}
