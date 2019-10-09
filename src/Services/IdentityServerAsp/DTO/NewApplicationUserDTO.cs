using Identity.API.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.DTO
{
    public class NewApplicationUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
        /// <summary>
        /// "existing" or "new"
        /// </summary>
        [StringRange(AllowedValues = new[] { "existing","new","",null})]
        public string ContactAssign { get; set; }
        public int? ExistingContactId { get; set; }
        public string NewContactFirstName { get; set; }
        public string NewContactLastName { get; set; }
        public string NewContactEmail { get; set; }
        public string NewContactPhone { get; set; }

        public bool AdministrationUser { get; set; }
        public bool ContactUser { get; set; }

    }
}
