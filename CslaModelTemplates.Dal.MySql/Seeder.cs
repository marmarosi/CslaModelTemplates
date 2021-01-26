using CslaModelTemplates.Dal.MySql.Entities;
using System;

namespace CslaModelTemplates.Dal.MySql
{
    /// <summary>
    /// Database seeder.
    /// </summary>
    public static class Seeder
    {
        private static Random random = new Random(DateTime.Now.Millisecond);

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

                #region Folder data

                CreateFolderLevel(ctx, 1, null, null, null);

                #endregion
            }
        }

        #region Folder helpers

        private static void CreateFolderLevel(
            MySqlContext ctx,
            int level,
            long? parentKey,
            long? rootKey,
            string parentPath
            )
        {
            int count = level == 1 ? 3 : random.Next(1, 5);
            for (int i = 0; i < count; i++)
            {
                int folderOrder = i + 1;
                Folder folder = CreateFolder(
                    parentKey,
                    rootKey,
                    folderOrder,
                    parentPath
                    );
                ctx.Folders.Add(folder);
                ctx.SaveChanges();

                if (level == 1)
                {
                    folder.RootKey = folder.FolderKey;
                    ctx.SaveChanges();
                }

                if (level < 4)
                {
                    string path = parentPath == null
                        ? folderOrder.ToString()
                        : $"{parentPath}.{folderOrder}";
                    CreateFolderLevel(
                        ctx,
                        level + 1,                      // level
                        folder.FolderKey,               // parentKey
                        rootKey ?? folder.FolderKey,    // rootKey
                        path                            // parentPath
                        );
                }
            }
        }

        private static Folder CreateFolder(
            long? parentKey,
            long? rootKey,
            int? folderOrder,
            string parentPath
            )
        {
            return new Folder
            {
                ParentKey = parentKey,
                RootKey = rootKey,
                FolderOrder = folderOrder,
                FolderName = parentPath == null
                    ? $"Folder entry number {folderOrder}"
                    : $"Folder entry number {parentPath}.{folderOrder}"
            };
        }

        #endregion
    }
}
