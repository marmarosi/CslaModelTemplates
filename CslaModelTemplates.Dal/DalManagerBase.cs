using Csla.Data.EntityFrameworkCore;
using CslaModelTemplates.Common;
using Microsoft.EntityFrameworkCore;
using System;

namespace CslaModelTemplates.Dal
{
    /// <summary>
    /// Represents a data access manager base object.
    /// </summary>
    public class DalManagerBase<C> : IDalManager, IDisposable, ISeeder 
        where C: DbContext
    {
        private Type RegistrarType = null;
        protected string ProviderMask = null;
        public DbContextManager<C> ContextManager { get; protected set; }

        /// <summary>
        /// Sets the type mask of the data access layer manager.
        /// </summary>
        /// <typeparam name="P">The type of the data access layer manager.</typeparam>
        /// <param name="dalManagerType">The classof the data access layer manager.</param>
        public void SetTypes<R, P>()
            where R : class
            where P : class
        {
            RegistrarType = typeof(R);

            Type providerType = typeof(P);
            ProviderMask = string.Format(
                "{0}, {1}",
                providerType.FullName.Replace(providerType.Name, @"{0}.{1}"),
                providerType.Namespace
                );
        }

        #region IDalManager

        /// <summary>
        /// Gets a data access object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the data access object to instantiate.</typeparam>
        /// <returns>The data access object of the specified type.</returns>
        public T GetProvider<T>() where T : class, IDal
        {
            Type result = typeof(T);
            string fullName = result.FullName;
            string nameSpace = result.Namespace;
            string className = fullName.Substring(fullName.LastIndexOf('.') + 2);
            string virtualPath = nameSpace.Substring(nameSpace.LastIndexOf('.') + 1);

            string typeName = ProviderMask.With(virtualPath, className);
            Type type = Type.GetType(typeName);
            if (type != null)
            {
                T provider = Activator.CreateInstance(type) as T;
                provider.DbContext = ContextManager.DbContext;
                return provider;
            }
            else
                throw new NotImplementedException(typeName);
        }

        /// <summary>
        /// Gets the database registrar.
        /// </summary>
        /// <returns>The database registrar.</returns>
        public IDalRegistrar GetDalRegistrar()
        {
            return Activator.CreateInstance(RegistrarType) as IDalRegistrar;
        }

        #endregion

        #region ISeeder

        /// <summary>
        /// Override this method to ensure the database schema and fill it with initial data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public virtual void ProductionSeed(string contentRootPath)
        { }

        /// <summary>
        /// Override this method to ensure the database schema and fill it with demo data.
        /// </summary>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public virtual void DevelopmentSeed(string contentRootPath)
        { }

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ContextManager.Dispose();
            ContextManager = default;
        }
    }
}
