using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Form.API.Models
{
    [Table("oz_user_history")]
    public partial class OzUserHistory
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("create_date", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        [Column("task_category")]
        public int? TaskCategory { get; set; }
        [Column("task_content")]
        [StringLength(1000)]
        public string TaskContent { get; set; }
        [Column("ip")]
        [StringLength(255)]
        public string Ip { get; set; }
        [Column("user_id")]
        [StringLength(255)]
        public string UserId { get; set; }
    }
}