using CslaModelTemplates.Common;
using System;

namespace CslaModelTemplates.Dal
{
    /// <summary>
    /// Defines the functionality of the manager object for a data access layer.
    /// </summary>
    public interface IDalManager : IDisposable
    {
        T GetProvider<T>() where T : class, IDal;
        IDalRegistrar GetDalRegistrar();
    }
}
