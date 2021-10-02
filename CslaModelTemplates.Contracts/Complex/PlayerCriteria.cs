using Csla;
using System;

namespace CslaModelTemplates.Contracts.Complex
{
    /// <summary>
    /// Represents the criteria of the editable player object.
    /// </summary>
    [Serializable]
    public class PlayerParams : CriteriaBase<PlayerParams>
    {
        public string PlayerId { get; set; }

        public PlayerCriteria Decode()
        {
            return new PlayerCriteria
            {
                PlayerKey = KeyHash.Decode(ID.Player, PlayerId) ?? 0
            };
        }
    }

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
