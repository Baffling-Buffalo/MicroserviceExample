using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Form.API.Models
{
    [Table("oz_item_history_ex")]
    public partial class OzItemHistoryEx
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("version")]
        public int? Version { get; set; }
        [Column("chkin_user_name")]
        [StringLength(63)]
        public string ChkinUserName { get; set; }
        [Column("chkin_time", TypeName = "datetime")]
        public DateTime? ChkinTime { get; set; }
        [Column("chkin_folder")]
        [StringLength(255)]
        public string ChkinFolder { get; set; }
        [Column("chkin_cmt")]
        [StringLength(1000)]
        public string ChkinCmt { get; set; }
        [Column("reserved")]
        [StringLength(1000)]
        public string Reserved { get; set; }
    }
}