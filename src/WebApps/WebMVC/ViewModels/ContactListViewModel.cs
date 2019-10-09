using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    public class ContactListViewModel
    {
        [Display(Name = "Contacts")]
        [Required(ErrorMessage ="Please select at least one contact")]
        public List<int> ContactIds { get; set; } //for hidden select on view

        public List<SelectListItem> Contacts { get; set; } //for hidden select on view

        public string ContactsString { get; set; }

        [Display(Name = "Lists")]
        [Required(ErrorMessage = "Please select at least one list")]
        public List<int> ListIds { get; set; }

        public List<SelectListItem> Lists { get; set; }

        public string ListsString { get; set; }

        public List<int> ListsOfContacts { get; set; }

    }
}
