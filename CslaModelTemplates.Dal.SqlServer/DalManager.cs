using Csla.Data.EntityFrameworkCore;

namespace CslaModelTemplates.Dal.SqlServer
{
    /// <summary>
    /// Represents the data access manager object for SQL Server databases.
    /// </summary>
    public sealed class DalManager : DalManagerBase<DbContextManager<SqlServerContext>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DalManager"/> class.
        /// </summary>
        public DalManager()
        {
            SetTypes<DalRegistrar, DalManager>();
            ConnectionManager = DbContextManager<SqlServerContext>.GetManager(DAL.SQLServer);
        }

        /// <summary>
        /// Ensures the database schema and fills it with initial data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public override void LiveSeed(
            string contentRootPath
            )
        {
            Seeder.Run(contentRootPath, false);
        }

        /// <summary>
        /// Ensures the database schema and fills it with demo data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public override void TestSeed(
            string contentRootPath
            )
        {
            Seeder.Run(contentRootPath, true);
        }
    }
}
