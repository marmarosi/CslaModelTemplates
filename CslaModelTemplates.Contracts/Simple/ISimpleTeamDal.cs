using CslaModelTemplates.Dal;

namespace CslaModelTemplates.Contracts.Simple
{
    /// <summary>
    /// Defines the data access functions of the editable team object.
    /// </summary>
    public interface ISimpleTeamDal : IDal
    {
        SimpleTeamDao Fetch(SimpleTeamCriteria criteria);
        void Insert(SimpleTeamDao dao);
        void Update(SimpleTeamDao dao);
        void Delete(SimpleTeamCriteria criteria);
    }
}
