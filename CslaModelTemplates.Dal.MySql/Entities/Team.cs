using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CslaModelTemplates.Dal.MySql.Entities
{
    [Table("Teams")]
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? TeamKey { get; set; }

        [MaxLength(10)]
        public string TeamCode { get; set; }

        [MaxLength(100)]
        public string TeamName { get; set; }

        [Timestamp]
        public DateTime Timestamp { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
