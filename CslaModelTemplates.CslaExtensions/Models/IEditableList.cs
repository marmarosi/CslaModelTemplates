using System.Collections.Generic;

namespace CslaModelTemplates.CslaExtensions.Models
{
    /// <summary>
    /// Defines the helper functions of editable lists.
    /// </summary>
    public interface IEditableList
    {
        IList<T> ToDto<T>() where T : class;
    }
}
