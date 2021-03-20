using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Dal.SqlServer
{
    public class SqlServerDal : DalBase
    {
        protected new SqlServerContext DbContext
        {
            get { return base.DbContext as SqlServerContext; }
        }
    }
}
