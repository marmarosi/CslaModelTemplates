using Microsoft.EntityFrameworkCore;

namespace CslaModelTemplates.Common.Dal
{
    public interface IDal
    {
        DbContext DbContext { get; set; }
    }
}
