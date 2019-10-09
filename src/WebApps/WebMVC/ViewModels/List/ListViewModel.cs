using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.List
{
    public class ListViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Please insert list name")]
        [Display(Name = "List name")]
        public string ListName { get; set; }

        [StringLength(50)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Parent")]
        public int? ParentId { get; set; }

        [Display(Name = "Contacts")]
        public List<int> ContactIds { get; set; }
    }
}
