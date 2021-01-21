using System.Collections.Generic;

namespace CslaModelTemplates.Common.Models
{
    /// <summary>
    /// Defines the data access functions of the read-only key-name choice object.
    /// </summary>
    public interface IKeyNameChoiceDal<T> where T : ChoiceCriteria
    {
        List<KeyNameOptionDao> Fetch(T criteria);
    }
}
