using Csla.Core;
using CslaModelTemplates.Common.Validations;
using System.Collections.Generic;

namespace CslaModelTemplates.Common.Models
{
    public interface IEditableModel
    {
        void CollectMessages(
            BusinessBase model,
            string prefix,
            ref List<ValidationMessage> messages
            );
    }
}
