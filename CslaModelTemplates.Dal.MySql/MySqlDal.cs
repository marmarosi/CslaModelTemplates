using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Dal.MySql
{
    public class MySqlDal : DalBase
    {
        protected new MySqlContext DbContext
        {
            get { return base.DbContext as MySqlContext; }
        }
    }
}
