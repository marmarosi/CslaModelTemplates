using CslaModelTemplates.Common.Dal;

namespace CslaModelTemplates.Contracts.ComplexView
{
    /// <summary>
    /// Defines the data access functions of the read-only team object.
    /// </summary>
    public interface ITeamViewDal : IDal
    {
        TeamViewDao Fetch(TeamViewCriteria criteria);
    }
}
