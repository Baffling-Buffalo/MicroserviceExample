using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Session.API.DTO.SessionContact
{
    public class UpdateSessionContactDTO
    {
        public int Id { get; set; }
        public int FillOrder { get; set; }
        public bool DocCompleted { get; set; }
        public int ContactId { get; set; }
        public int SessionDocumentId { get; set; }
    }
}
