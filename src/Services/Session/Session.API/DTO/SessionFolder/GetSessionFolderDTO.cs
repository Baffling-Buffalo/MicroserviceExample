using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Session.API.DTO.SessionFolder
{
    public class GetSessionFolderDTO
    {
        public int Id { get; set; }
        public string FolderName { get; set; }
        public int SessionMainId { get; set; }
        public string SessionMainName { get; set; }
    }
}
