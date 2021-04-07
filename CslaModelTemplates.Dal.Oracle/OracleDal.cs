using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Dal.Oracle
{
    public class OracleDal : DalBase
    {
        protected new OracleContext DbContext
        {
            get { return base.DbContext as OracleContext; }
        }
    }
}
