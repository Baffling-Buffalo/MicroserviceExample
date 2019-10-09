using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Session.API.DTO.SessionDocument
{
    public class GetSessionDocumentDTO
    {
        public int Id { get; set; }
        public int OzItemId { get; set; }
        public int SessionFolderId { get; set; }
        public string SessionFolderName { get; set; }
        public string OzItemContent { get; set; }
    }
}
