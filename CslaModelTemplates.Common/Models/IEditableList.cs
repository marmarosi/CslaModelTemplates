using System.Collections.Generic;

namespace CslaModelTemplates.Common.Models
{
    /// <summary>
    /// Defines the helper functions of editable lists.
    /// </summary>
    public interface IEditableList
    {
        IList<T> ToDto<T>() where T : class;
    }
}
