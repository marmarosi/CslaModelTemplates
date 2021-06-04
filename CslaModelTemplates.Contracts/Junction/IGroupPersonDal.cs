using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Contracts.Junction
{
    /// <summary>
    /// Defines the data access functions of the editable group-person object.
    /// </summary>
    public interface IGroupPersonDal : IDal
    {
        void Insert(GroupPersonDao dao);
        void Delete(GroupPersonDao dao);
    }
}
