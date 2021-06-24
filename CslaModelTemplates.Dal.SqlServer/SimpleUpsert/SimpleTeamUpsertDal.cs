using CslaModelTemplates.Common;
using CslaModelTemplates.Contracts.SimpleUpsert;
using CslaModelTemplates.Dal.Exceptions;
using CslaModelTemplates.Dal.SqlServer.Entities;
using CslaModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CslaModelTemplates.Dal.SqlServer.SimpleUpsert
{
    /// <summary>
    /// Implements the data access functions of the editable team object.
    /// </summary>
    public class SimpleTeamUpsertDal : SqlServerDal, ISimpleTeamUpsertDal
    {
        #region Fetch

        /// <summary>
        /// Gets the specified team.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        /// <returns>The requested team.</returns>
        public SimpleTeamUpsertDao Fetch(
            SimpleTeamUpsertCriteria criteria
            )
        {
            // Get the specified team.
            SimpleTeamUpsertDao team = DbContext.Teams
                .Where(e =>
                    e.TeamCode == criteria.TeamCode
                 )
                .Select(e => new SimpleTeamUpsertDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName,
                    Timestamp = e.Timestamp
                })
                .AsNoTracking()
                .FirstOrDefault();

            if (team == null)
                throw new DataNotFoundException(DalText.SimpleTeamUpsert_NotFound);

            return team;
        }

        #endregion Fetch

        #region Insert

        /// <summary>
        /// Creates a new team using the specified data.
        /// </summary>
        /// <param name="dao">The data of the team.</param>
        public void Insert(
            SimpleTeamUpsertDao dao
            )
        {
            // Check unique team code.
            Team team = DbContext.Teams
                .Where(e =>
                    e.TeamCode == dao.TeamCode
                )
                .FirstOrDefault();
            if (team != null)
                throw new DataExistException(DalText.SimpleTeamUpsert_TeamCodeExists.With(dao.TeamCode));

            // Create the new team.
            team = new Team
            {
                TeamCode = dao.TeamCode,
                TeamName = dao.TeamName
            };
            DbContext.Teams.Add(team);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new InsertFailedException(DalText.SimpleTeamUpsert_InsertFailed);

            // Return new data.
            dao.TeamKey = team.TeamKey;
            dao.Timestamp = team.Timestamp;
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing team using the specified data.
        /// </summary>
        /// <param name="dao">The data of the team.</param>
        public void Update(
            SimpleTeamUpsertDao dao
            )
        {
            // Get the specified team.
            Team team = DbContext.Teams
                .Where(e =>
                    e.TeamCode == dao.TeamCode
                )
                .FirstOrDefault();
            if (team == null)
                throw new DataNotFoundException(DalText.SimpleTeamUpsert_NotFound);
            if (team.Timestamp != dao.Timestamp)
                throw new ConcurrencyException(DalText.SimpleTeamUpsert_Concurrency);

            // Check unique team code.
            if (team.TeamCode != dao.TeamCode)
            {
                int exist = DbContext.Teams
                    .Where(e => e.TeamCode == dao.TeamCode && e.TeamKey != team.TeamKey)
                    .Count();
                if (exist > 0)
                    throw new DataExistException(DalText.SimpleTeamUpsert_TeamCodeExists.With(dao.TeamCode));
            }

            // Update the team.
            team.TeamCode = dao.TeamCode;
            team.TeamName = dao.TeamName;

            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new UpdateFailedException(DalText.SimpleTeamUpsert_UpdateFailed);

            // Return new data.
            dao.Timestamp = team.Timestamp;
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified team.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        public void Delete(
            SimpleTeamUpsertCriteria criteria
            )
        {
            // Get the specified team.
            Team team = DbContext.Teams
                .Where(e =>
                    e.TeamCode == criteria.TeamCode
                 )
                .AsNoTracking()
                .FirstOrDefault();

            if (team == null)
                throw new DataNotFoundException(DalText.SimpleTeamUpsert_NotFound);

            // Check or delete references
            //int dependents = 0;

            //dependents = DbContext.Others.Count(e => e.TeamCode == criteria.TeamCode);
            //if (dependents > 0)
            //    throw new DeleteFailedException(DalText.SimpleTeamUpsert_Delete_Others);

            // Delete the team.
            DbContext.Teams.Remove(team);
            int count = DbContext.SaveChanges();
            if (count == 0)
                throw new DeleteFailedException(DalText.SimpleTeamUpsert_DeleteFailed);
        }

        #endregion Delete
    }
}
