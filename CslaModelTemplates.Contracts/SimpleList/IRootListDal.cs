﻿using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.SimpleList
{
    /// <summary>
    /// Defines the data access functions of the read-only root collection.
    /// </summary>
    public interface IRootListDal
    {
        List<RootListItemDao> Get(RootListCriteria criteria);
    }
}
