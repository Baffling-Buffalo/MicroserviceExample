using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Session.API.DTO.SessionMain
{
    public class UpdateSessionMainDTO
    {
        public int Id { get; set; }
        [Required]
        public string SessionName { get; set; }
        public string Description { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeEnd { get; set; }
        public bool? IsInfinite { get; set; }
        public bool? IsTemplate { get; set; }
    }
}
