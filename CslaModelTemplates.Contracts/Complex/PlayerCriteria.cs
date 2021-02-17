using Csla;
using System;

namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Represents the criteria of the editable player object.
    /// </summary>
    [Serializable]
    public class PlayerCriteria : CriteriaBase<PlayerCriteria>
    {
        public long PlayerKey { get; set; }

        public PlayerCriteria()
        { }

        public PlayerCriteria(
            long playerKey
            )
        {
            PlayerKey = playerKey;
        }
    }
}
