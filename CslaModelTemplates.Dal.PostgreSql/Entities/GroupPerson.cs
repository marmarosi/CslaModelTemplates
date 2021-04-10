using System.ComponentModel.DataAnnotations.Schema;

namespace CslaModelTemplates.Dal.PostgreSql.Entities
{
    [Table("GroupPersons")]
    public class GroupPerson
    {
        public long? GroupKey { get; set; }

        [ForeignKey("GroupKey")]
        public Group Group { get; set; }

        public long? PersonKey { get; set; }

        [ForeignKey("PersonKey")]
        public Person Person { get; set; }
    }
}
