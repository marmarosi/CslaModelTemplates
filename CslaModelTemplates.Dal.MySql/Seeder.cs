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

                #region Node data

                CreateNodeLevel(ctx, 1, null, null);

                #endregion
            }
        }

        #region Node helpers

        private static void CreateNodeLevel(
            MySqlContext ctx,
            int level,
            long? parentKey,
            string parentPath
            )
        {
            int count = level == 1 ? 3 : random.Next(1, 5);
            for (int i = 0; i < count; i++)
            {
                int nodeOrder = i + 1;
                Node node = CreateNode(parentKey, nodeOrder, parentPath);
                ctx.Nodes.Add(node);
                ctx.SaveChanges();

                if (level < 4)
                {
                    string path = parentPath == null
                        ? nodeOrder.ToString()
                        : $"{parentPath}.{nodeOrder}";
                    CreateNodeLevel(ctx, level + 1, node.NodeKey, path);
                }
            }
        }

        private static Node CreateNode(
            long? parentKey,
            int? nodeOrder,
            string parentPath
            )
        {
            return new Node
            {
                ParentKey = parentKey,
                NodeOrder = nodeOrder,
                NodeName = parentPath == null
                    ? $"Node entry number {nodeOrder}"
                    : $"Node entry number {parentPath}.{nodeOrder}"
            };
        }

        #endregion
    }
}
