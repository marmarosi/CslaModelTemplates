using CslaModelTemplates.Dal;

namespace CslaModelTemplates.Dal.Oracle
{
    /// <summary>
    /// Represents the data access layer.
    /// </summary>
    public class OracleDal : DalBase
    {
        protected new OracleContext DbContext
        {
            get { return base.DbContext as OracleContext; }
        }
    }
}
