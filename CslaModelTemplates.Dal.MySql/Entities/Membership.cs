using System.ComponentModel.DataAnnotations.Schema;

namespace CslaModelTemplates.Dal.MySql.Entities
{
    [Table("Memberships")]
    public class Membership
    {
        public long? GroupKey { get; set; }

        [ForeignKey("GroupKey")]
        public virtual Group Group { get; set; }

        public long? PersonKey { get; set; }

        [ForeignKey("PersonKey")]
        public virtual Person Person { get; set; }
    }
}
