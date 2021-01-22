using CslaModelTemplates.Dal.MySql.Entities;

namespace CslaModelTemplates.Dal.MySql
{
    /// <summary>
    /// Database seeder.
    /// </summary>
    public static class Seeder
    {
        /// <summary>
        /// Initializes the database schema and fills it with demo data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        /// <param name="isDevelopment">Indicates whether the app is running in development mode.</param>
        public static void Run(
            string contentRootPath,
            bool isDevelopment
            )
        {
            using (MySqlContext ctx = new MySqlContext(DAL.MySQL))
            {
                if (isDevelopment)
                    ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();

                #region Root data

                for (int i = 0; i < 100; i++)
                {
                    int serialNumber = i + 1;
                    ctx.Roots.Add(new Root
                    {
                        RootCode = $"R-{serialNumber.ToString("0000")}",
                        RootName = $"Root entry number {serialNumber}",
                    });
                }
                ctx.SaveChanges();

                #endregion
            }
        }
    }
}
