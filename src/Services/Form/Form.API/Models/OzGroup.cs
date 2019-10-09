using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Form.API.Models
{
    [Table("oz_group")]
    public partial class OzGroup
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("p_id")]
        public int? PId { get; set; }
        [Column("name")]
        [StringLength(127)]
        public string Name { get; set; }
        [Column("full_path")]
        [StringLength(255)]
        public string FullPath { get; set; }
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; }
        [Column("update_time", TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }
        [Column("org")]
        public int? Org { get; set; }
        [Column("u_id")]
        public int? UId { get; set; }
        [Column("lft")]
        public int? Lft { get; set; }
        [Column("rgt")]
        public int? Rgt { get; set; }
    }
}