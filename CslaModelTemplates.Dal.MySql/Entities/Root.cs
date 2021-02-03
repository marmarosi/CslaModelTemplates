using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CslaModelTemplates.Dal.MySql.Entities
{
    [Table("Roots")]
    public class Root
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? RootKey { get; set; }

        [MaxLength(10)]
        public string RootCode { get; set; }

        [MaxLength(100)]
        public string RootName { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public List<RootItem> Items { get; set; }
    }
}
