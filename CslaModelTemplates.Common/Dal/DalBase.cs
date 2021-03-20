using Microsoft.EntityFrameworkCore;

namespace CslaModelTemplates.Common.Dal
{
    public class DalBase : IDal
    {
        public DbContext DbContext { get; set; }
    }
}
