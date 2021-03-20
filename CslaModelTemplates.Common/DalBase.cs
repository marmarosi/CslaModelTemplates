using Microsoft.EntityFrameworkCore;

namespace CslaModelTemplates.Common
{
    public class DalBase : IDal
    {
        public DbContext DbContext { get; set; }
    }
}
