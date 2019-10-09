using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Form.API.Models
{
    [Table("oz_user")]
    public partial class OzUser
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("p_id")]
        public int? PId { get; set; }
        [Column("name")]
        [StringLength(63)]
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
        [Column("passwd")]
        [StringLength(31)]
        public string Passwd { get; set; }
        [Column("user_session")]
        public int? UserSession { get; set; }
        [Column("is_logged_in")]
        public int? IsLoggedIn { get; set; }
        [Column("login_ip")]
        [StringLength(16)]
        public string LoginIp { get; set; }
        [Column("login_enabled")]
        public int? LoginEnabled { get; set; }
        [Column("last_login_time", TypeName = "datetime")]
        public DateTime? LastLoginTime { get; set; }
        [Column("allow_ip")]
        [StringLength(255)]
        public string AllowIp { get; set; }
        [Column("admin_id")]
        public int? AdminId { get; set; }
    }
}