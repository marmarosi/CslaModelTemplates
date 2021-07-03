using CslaModelTemplates.Dal;

namespace CslaModelTemplates.Contracts.SimpleUpsert
{
    /// <summary>
    /// Defines the data access functions of the editable team object.
    /// </summary>
    public interface ISimpleTeamUpsertDal : IDal
    {
        SimpleTeamUpsertDao Fetch(SimpleTeamUpsertCriteria criteria);
        void Insert(SimpleTeamUpsertDao dao);
        void Update(SimpleTeamUpsertDao dao);
        void Delete(SimpleTeamUpsertCriteria criteria);
    }
}
