using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Session.API.DTO.SessionDocument
{
    public class UpdateSessionDocumentDTO
    {
        public int Id { get; set; }
        [Required]
        public int OzItemId { get; set; }
        [Required]
        public int SessionFolderId { get; set; }
        public string OzItemContent { get; set; }
    }
}
