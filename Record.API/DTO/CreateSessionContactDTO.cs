using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Record.API.DTO.SessionContact
{
    public class CreateSessionContactDTO
    {
        public int FillOrder { get; set; }
        public bool DocCompleted { get; set; }
        public int ContactId { get; set; }
        public int SessionDocumentId { get; set; }
    }
}
