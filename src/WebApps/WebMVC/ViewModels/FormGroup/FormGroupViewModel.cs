using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.FormGroup
{
    public class FormGroupViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Please insert group name")]
        [Display(Name = "Group name")]
        public string GroupName { get; set; }

        [StringLength(50)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Parent")]
        public int? ParentId { get; set; }
    }
}
