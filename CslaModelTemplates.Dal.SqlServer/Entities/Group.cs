using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CslaModelTemplates.Dal.SqlServer.Entities
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? GroupKey { get; set; }

        [MaxLength(10)]
        public string GroupCode { get; set; }

        [MaxLength(100)]
        public string GroupName { get; set; }

        public DateTime Timestamp { get; set; }

        public List<Person> Members { get; set; }
    }
}
