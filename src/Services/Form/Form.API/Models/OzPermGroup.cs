using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Form.API.Models
{
    [Table("oz_perm_group")]
    public partial class OzPermGroup
    {
        [Key]
        [Column("executer_id")]
        public int ExecuterId { get; set; }
        [Column("object_id")]
        public int? ObjectId { get; set; }
        [Column("mask1")]
        public int? Mask1 { get; set; }
        [Column("mask2")]
        public int? Mask2 { get; set; }
        [Column("mask3")]
        public int? Mask3 { get; set; }
        [Column("mask4")]
        public int? Mask4 { get; set; }
        [Column("mask5")]
        public int? Mask5 { get; set; }
    }
}