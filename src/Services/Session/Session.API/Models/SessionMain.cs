using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Session.API.Models
{
    [Table("session_main")]
    public partial class SessionMain
    {
        public SessionMain()
        {
            SessionFolders = new HashSet<SessionFolder>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("session_name")]
        [StringLength(50)]
        public string SessionName { get; set; }
        [Column("description")]
        [StringLength(250)]
        public string Description { get; set; }
        [Column("date_start", TypeName = "date")]
        public DateTime? DateStart { get; set; }
        [Column("date_end", TypeName = "date")]
        public DateTime? DateEnd { get; set; }
        [Column("time_start", TypeName = "time(0)")]
        public TimeSpan? TimeStart { get; set; }
        [Column("time_end", TypeName = "time(0)")]
        public TimeSpan? TimeEnd { get; set; }
        [Column("is_infinite")]
        public bool? IsInfinite { get; set; }
        [Column("is_template")]
        public bool? IsTemplate { get; set; }

        [InverseProperty("FSessionMainNavigation")]
        public virtual ICollection<SessionFolder> SessionFolders { get; set; }
    }
}