using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class UsersRolesViewModel
    {
        public string ReturnUrl { get; set; }
        [Display(Name = "Users")]
        [Required(ErrorMessage = "Select 1 or more users")]
        public string[] UserIds { get; set; }
        [Display(Name = "Roles")]
        [Required(ErrorMessage = "Select 1 or more roles")]
        public string[] RoleIds { get; set; }

        public string[] UserStrings { get; set; }
    }
}
