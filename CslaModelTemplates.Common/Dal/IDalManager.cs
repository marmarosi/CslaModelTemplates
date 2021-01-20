using System;

namespace CslaModelTemplates.Common.Dal
{
    /// <summary>
    /// Defines the functionality of the manager object for a data access layer.
    /// </summary>
    public interface IDalManager : IDisposable
    {
        T GetProvider<T>() where T : class;
        IDalRegistrar GetDalRegistrar();
    }
}
