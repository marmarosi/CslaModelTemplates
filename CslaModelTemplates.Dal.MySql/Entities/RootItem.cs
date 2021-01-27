using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CslaModelTemplates.Dal.MySql.Entities
{
    [Table("RootItems")]
    public class RootItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RootItemKey { get; set; }

        public long RootKey { get; set; }

        [MaxLength(10)]
        public string RootItemCode { get; set; }

        [MaxLength(100)]
        public string RootItemName { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        [ForeignKey("RootKey")]
        public Root Root { get; set; }
    }
}
