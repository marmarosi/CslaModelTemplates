using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CslaModelTemplates.Dal.MySql.Entities
{
    [Table("Folders")]
    public class Folder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? FolderKey { get; set; }

        public long? ParentKey { get; set; }

        public long? RootKey { get; set; }

        public int? FolderOrder { get; set; }

        [MaxLength(100)]
        public string FolderName { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        [ForeignKey("ParentKey")]
        public Folder Parent { get; set; }

        public List<Folder> Children { get; set; }
    }
}
