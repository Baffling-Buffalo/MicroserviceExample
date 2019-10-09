using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.Role
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "System Role")]
        public bool SystemRole { get; set; }

        [Display(Name = "Allowed Actions")]
        public string[] RoleActions { get; set; }
    }
}
