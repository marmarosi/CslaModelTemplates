﻿namespace CslaModelTemplates.Common.Dal
{
    /// <summary>
    /// Defines the functionality of a database seeder.
    /// </summary>
    public interface ISeeder
    {
        void ProductionSeed(string contentRootPath);
        void TestSeed(string contentRootPath);
    }
}
