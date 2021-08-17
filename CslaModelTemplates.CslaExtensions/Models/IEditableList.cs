using System.Collections.Generic;
using System.Threading.Tasks;

namespace CslaModelTemplates.CslaExtensions.Models
{
    /// <summary>
    /// Defines the helper functions of editable lists.
    /// </summary>
    public interface IEditableList
    {
        IList<T> ToDto<T>() where T : class;
        Task Update<D>(List<D> list, string keyName) where D : class;
    }
}
