using CslaModelTemplates.Dal;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts
{
    /// <summary>
    /// Defines the data access functions of the read-only code-name choice object.
    /// </summary>
    public interface ICodeNameChoiceDal<T> : IDal
        where T : ChoiceCriteria
    {
        List<CodeNameOptionDao> Fetch(T criteria);
    }
}
