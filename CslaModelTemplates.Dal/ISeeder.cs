namespace CslaModelTemplates.Dal
{
    /// <summary>
    /// Defines the functionality of a database seeder.
    /// </summary>
    public interface ISeeder
    {
        void LiveSeed(string contentRootPath);
        void TestSeed(string contentRootPath);
    }
}
