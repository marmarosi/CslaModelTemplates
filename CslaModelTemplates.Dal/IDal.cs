using Microsoft.EntityFrameworkCore;

namespace CslaModelTemplates.Dal
{
    /// <summary>
    /// Defines the functionality of a data access layer.
    /// </summary>
    public interface IDal
    {
        DbContext DbContext { get; set; }
    }
}
