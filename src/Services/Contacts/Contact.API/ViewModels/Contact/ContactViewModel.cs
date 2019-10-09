using Contact.API.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.ViewModels.Contact
{
    public class ContactViewModel
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Please insert first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Please insert last name")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Please insert e-mail")]
        [EmailAddress(ErrorMessage = "E-mail is not valid")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public IEnumerable<int> ListIds { get; set; }

        [Display(Name = "Lists")]
        public IEnumerable<SelectListItem> Lists { get; set; }

    }
}
