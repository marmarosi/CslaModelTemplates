using CslaModelTemplates.Dal.PostgreSql.Entities;
using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Dal.PostgreSql
{
    /// <summary>
    /// Database seeder.
    /// </summary>
    public static class PostgreSqlSeeder
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
            using (PostgreSqlContext ctx = new PostgreSqlContext(DAL.PostgreSQL))
            {
                if (isDevelopment)
                    ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
                if (!isDevelopment)
                    return;

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

                #region Group data

                List<long> groupKeys = new List<long>();
                for (int i = 0; i < 20; i++)
                {
                    int serialNumber = i + 1;
                    Group group = new Group
                    {
                        GroupCode = $"G-{serialNumber.ToString("00")}",
                        GroupName = $"Group No. {serialNumber}",
                    };
                    ctx.Groups.Add(group);
                    ctx.SaveChanges();
                    groupKeys.Add(group.GroupKey.Value);
                }

                #endregion

                #region Person data

                List<long> personKeys = new List<long>();
                for (int i = 0; i < 20; i++)
                {
                    int serialNumber = i + 1;
                    Person person = new Person
                    {
                        PersonCode = $"XY-{serialNumber.ToString("00")}",
                        PersonName = $"Person #{serialNumber}",
                    };
                    ctx.Persons.Add(person);
                    ctx.SaveChanges();
                    personKeys.Add(person.PersonKey.Value);
                }

                #endregion

                #region GroupPerson data

                foreach (long groupKey in groupKeys)
                {
                    int count = random.Next(1, 5);
                    List<long> tempKeys = personKeys.GetRange(0, 20);
                    for (int j = 0; j < count; j++)
                    {
                        int index = random.Next(1, 20 - j);
                        long personKey = tempKeys[index];
                        ctx.GroupPersons.Add(new GroupPerson
                        {
                            GroupKey = groupKey,
                            PersonKey = personKey
                        });
                        tempKeys.Remove(personKey);
                    }
                }
                ctx.SaveChanges();

                #endregion
            }
        }

        #region Folder helpers

        private static void CreateFolderLevel(
            PostgreSqlContext ctx,
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
