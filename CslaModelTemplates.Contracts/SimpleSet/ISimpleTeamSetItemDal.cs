using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Contracts.SimpleSet
{
    /// <summary>
    /// Defines the data access functions of the editable team object.
    /// </summary>
    public interface ISimpleTeamSetItemDal : IDal
    {
        void Insert(SimpleTeamSetItemDao dao);
        void Update(SimpleTeamSetItemDao dao);
        void Delete(SimpleTeamSetItemCriteria criteria);
    }
}
