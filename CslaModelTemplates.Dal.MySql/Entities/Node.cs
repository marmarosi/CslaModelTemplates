using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CslaModelTemplates.Dal.MySql.Entities
{
    [Table("Nodes")]
    public class Node
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? NodeKey { get; set; }

        public long? ParentKey { get; set; }

        public int? NodeOrder { get; set; }

        [MaxLength(100)]
        public string NodeName { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        [ForeignKey("ParentKey")]
        public Node Parent { get; set; }

        public List<Node> Children { get; set; }
    }
}
