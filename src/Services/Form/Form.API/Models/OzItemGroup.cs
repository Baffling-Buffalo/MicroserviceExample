using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Form.API.Models
{
    [Table("oz_item_group")]
    public partial class OzItemGroup
    {
        [Column("f_oz_item_id")]
        public int FOzItemId { get; set; }
        [Column("f_group_id")]
        public int FGroupId { get; set; }

        [ForeignKey("FGroupId")]
        [InverseProperty("OzItemGroups")]
        public virtual Group FGroup { get; set; }
        [ForeignKey("FOzItemId")]
        [InverseProperty("OzItemGroups")]
        public virtual OzItem FOzItem { get; set; }
    }
}