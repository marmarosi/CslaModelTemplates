using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Dal.PostgreSql
{
    public class PostgreSqlDal : DalBase
    {
        protected new PostgreSqlContext DbContext
        {
            get { return base.DbContext as PostgreSqlContext; }
        }
    }
}
