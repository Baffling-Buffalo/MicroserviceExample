using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.List
{
    public class ListDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "List name")]
        public string ListName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "ParentId")]
        public int? ParentId { get; set; }

        [Display(Name = "Parent")]
        public string ParentName { get; set; }

        [Display(Name = "Sublists")]
        public string ChildLists { get; set; }

    }
}
