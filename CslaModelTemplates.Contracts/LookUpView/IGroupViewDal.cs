using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Contracts.LookUpView
{
    /// <summary>
    /// Defines the data access functions of the read-only group object.
    /// </summary>
    public interface IGroupViewDal : IDal
    {
        GroupViewDao Fetch(GroupViewCriteria criteria);
    }
}
