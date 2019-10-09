using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string ReturnUrl { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords don't match")]
        [Display(Name = "Repeat new password")]
        public string NewPasswordRepeat { get; set; }
    }
}
