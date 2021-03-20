using Microsoft.EntityFrameworkCore;

namespace CslaModelTemplates.Common
{
    public interface IDal
    {
        DbContext DbContext { get; set; }
    }
}
