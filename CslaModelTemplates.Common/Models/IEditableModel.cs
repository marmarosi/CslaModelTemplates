using Csla.Core;
using CslaModelTemplates.Common.Validations;
using System.Collections.Generic;

namespace CslaModelTemplates.Common.Models
{
    /// <summary>
    /// Defines the helper functions of editable models.
    /// </summary>
    public interface IEditableModel
    {
        void CollectMessages(
            BusinessBase model,
            string prefix,
            ref List<ValidationMessage> messages
            );
        T ToDto<T>() where T : class;
    }
}
