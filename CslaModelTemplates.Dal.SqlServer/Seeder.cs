using CslaModelTemplates.Dal.SqlServer.Entities;
using System;

namespace CslaModelTemplates.Dal.SqlServer
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
            using (SqlServerContext ctx = new SqlServerContext(DAL.SQLServer))
            {
                if (isDevelopment)
                    ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();

                #region Team data

                for (int i = 0; i < 50; i++)
                {
                    int serialNumber = i + 1;
                    Team team = new Team
                    {
                        TeamCode = $"T-{serialNumber.ToString("0000")}",
                        TeamName = $"Team entry number {serialNumber}",
                    };
                    ctx.Teams.Add(team);
                    ctx.SaveChanges();

                    int count = random.Next(1, 5);
                    for (int j = 0; j < count; j++)
                    {
                        int index = j + 1;
                        ctx.Players.Add(new Player
                        {
                            TeamKey = team.TeamKey,
                            PlayerCode = $"P-{serialNumber.ToString("0000")}-{index}",
                            PlayerName = $"Item entry number {serialNumber}.{index}",
                        });
                    }
                    ctx.SaveChanges();
                }

                #endregion

                #region Folder data

                CreateFolderLevel(ctx, 1, null, null, null);

                #endregion
            }
        }

        #region Folder helpers

        private static void CreateFolderLevel(
            SqlServerContext ctx,
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
                        rootKey ?? folder.FolderKey,    // teamKey
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
