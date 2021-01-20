﻿using Csla;
using System;

namespace CslaModelTemplates.Contracts.SimpleList
{
    /// <summary>
    /// Represents the criteria of the read-only root collection.
    /// </summary>
    [Serializable]
    public class RootListCriteria : CriteriaBase<RootListCriteria>
    {
        public string RootName { get; set; }
    }
}
