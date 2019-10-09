using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Session.API.DTO.SessionDocumentRole
{
    public class UpdateSessionDocumentRoleDTO
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        public string FieldType { get; set; }
        public string FieldDescription { get; set; }
        public int SessionContactId { get; set; }
    }
}
