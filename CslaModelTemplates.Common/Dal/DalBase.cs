using Microsoft.EntityFrameworkCore;

namespace CslaModelTemplates.Common.Dal
{
    /// <summary>
    /// Implements the functionality of a data access layer.
    /// </summary>
    public class DalBase : IDal
    {
        public DbContext DbContext { get; set; }
    }
}
