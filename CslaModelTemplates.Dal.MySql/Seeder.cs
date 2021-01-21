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
            MySqlContext ctx = new MySqlContext(DAL.MySQL);

            if (isDevelopment)
                ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            //TestDataGenerator<ApiDbContext>.Generate(ctx);
        }
    }
}
