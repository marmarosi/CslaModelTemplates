using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Contracts.Junction
{
    /// <summary>
    /// Defines the data access functions of the editable member object.
    /// </summary>
    public interface IMemberDal : IDal
    {
        void Insert(MemberDao dao);
        void Delete(MemberDao dao);
    }
}
