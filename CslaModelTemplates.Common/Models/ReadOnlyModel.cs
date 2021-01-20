using Csla;
using System;
using System.Text.Json.Serialization;

namespace CslaModelTemplates.Common.Models
{
    /// <summary>
    /// Wrapper for read-only model to hide server side properties.
    /// </summary>
    /// <typeparam name="T">The type of the business object.</typeparam>
    [Serializable]
    public abstract class ReadOnlyModel<T> : ReadOnlyBase<T> where T: ReadOnlyBase<T>
    {
        [JsonIgnore]
        public override bool IsBusy => base.IsBusy;

        [JsonIgnore]
        public override bool IsSelfBusy => base.IsSelfBusy;
    }
}
