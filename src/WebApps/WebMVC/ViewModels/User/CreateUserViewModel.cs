using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.User
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Repeat Password")]
        [Compare(nameof(Password),ErrorMessage = "Passwords do not match")]
        public string RepeatPassword { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Status")]
        public bool Active { get; set; } = true;

        [Display(Name = "Assign Contact")]
        public string ContactAssign { get; set; }

        public int ExistingContactId { get; set; }

        [Display(Name = "Contact's First Name")]
        public string NewContactFirstName { get; set; }
        [Display(Name = "Contact's Last Name")]
        public string NewContactLastName { get; set; }
        [Display(Name = "Contact's Phone Number")]
        public string NewContactPhone { get; set; }
        [Display(Name = "Contact's Email")]
        public string NewContactEmail { get; set; }
    }
}
