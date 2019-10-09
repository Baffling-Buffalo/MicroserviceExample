using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Session.API.DTO.SessionFolder
{
    public class UpdateSessionFolderDTO
    {
        public int Id { get; set; }
        [Required]
        public string FolderName { get; set; }
        [Required]
        public int SessionMainId { get; set; }
    }
}
