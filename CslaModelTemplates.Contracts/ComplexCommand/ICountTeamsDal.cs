using CslaModelTemplates.Common.Dal;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts.ComplexCommand
{
    /// <summary>
    /// Defines the data access functions of the count teams by player count command.
    /// </summary>
    public interface ICountTeamsDal : IDal
    {
        List<CountTeamsListItemDao> Execute(CountTeamsCriteria criteria);
    }
}
