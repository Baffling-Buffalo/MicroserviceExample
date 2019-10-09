using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Form.API.Models
{
    [Table("oz_item")]
    public partial class OzItem
    {
        public OzItem()
        {
            OzItemGroups = new HashSet<OzItemGroup>();
        }

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
        [Column("item_type")]
        public int? ItemType { get; set; }
        [Column("is_chked_out")]
        public int? IsChkedOut { get; set; }
        [Column("chkout_folder")]
        [StringLength(255)]
        public string ChkoutFolder { get; set; }
        [Column("chkout_uid")]
        public int? ChkoutUid { get; set; }
        [Column("version")]
        public int? Version { get; set; }
        [Column("chkout_cmt")]
        [StringLength(1000)]
        public string ChkoutCmt { get; set; }

        [InverseProperty("FOzItem")]
        public virtual ICollection<OzItemGroup> OzItemGroups { get; set; }
    }
}