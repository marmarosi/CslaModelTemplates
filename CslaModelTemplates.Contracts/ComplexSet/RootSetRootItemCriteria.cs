using Csla;
using System;
using System.Text.Json.Serialization;

namespace CslaModelTemplates.Contracts.ComplexSet
{
    /// <summary>
    /// Represents the criteria of the editable root item object.
    /// </summary>
    [Serializable]
    public class RootSetRootItemCriteria : CriteriaBase<RootSetRootItemCriteria>
    {
        public long RootItemKey { get; set; }
        [JsonIgnore]
        public string __rootCode { get; set; } // for error messages
        [JsonIgnore]
        public string __rootItemCode { get; set; } // for error messages

        public RootSetRootItemCriteria()
        { }

        public RootSetRootItemCriteria(
            long rootItemKey
            )
        {
            RootItemKey = rootItemKey;
        }
    }
}
