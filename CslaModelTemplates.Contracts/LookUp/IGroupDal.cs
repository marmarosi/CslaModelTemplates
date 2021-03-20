using CslaModelTemplates.Common;

namespace CslaModelTemplates.Contracts.LookUp
{
    /// <summary>
    /// Defines the data access functions of the editable group object.
    /// </summary>
    public interface IGroupDal : IDal
    {
        GroupDao Fetch(GroupCriteria criteria);
        void Insert(GroupDao dao);
        void Update(GroupDao dao);
        void Delete(GroupCriteria criteria);
    }
}
