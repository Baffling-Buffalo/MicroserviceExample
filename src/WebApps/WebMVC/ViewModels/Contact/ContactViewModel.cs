using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.Contact
{
    public class ContactViewModel
    {
        public int Id { get; set; }

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
        [Display(Name = "E-mail address")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        /*[Required(ErrorMessage = "Please pick one status")]*/
        [Display(Name = "Status")]
        public bool Active { get; set; }

        [Display(Name = "Lists")]
        public List<int> ListIds { get; set; }



        //LOGIN CREDENTIALS
        [Required(ErrorMessage = "Please pick one status")]
        [Display(Name = "Allow Login")]
        public bool AllowLogin { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Repeat Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string RepeatPassword { get; set; }
    }
}
