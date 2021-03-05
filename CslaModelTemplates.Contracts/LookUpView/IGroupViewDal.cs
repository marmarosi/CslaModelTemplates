using System;
using System.Collections.Generic;
using System.Text;

namespace CslaModelTemplates.Contracts.LookUpView
{
    /// <summary>
    /// Defines the data access functions of the read-only group object.
    /// </summary>
    public interface IGroupViewDal
    {
        GroupViewDao Fetch(GroupViewCriteria criteria);
    }
}
