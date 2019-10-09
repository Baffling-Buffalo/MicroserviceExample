using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Services.ModelDTOs
{
    public class NewApplicationUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
        public string ContactAssign { get; set; }
        public int? ExistingContactId { get; set; }
        public string NewContactFirstName { get; set; }
        public string NewContactLastName { get; set; }
        public string NewContactEmail { get; set; }
        public string NewContactPhone { get; set; }

        public bool AdministrationUser { get; set; } = true;
        public bool ContactUser { get; set; }
    }
}
