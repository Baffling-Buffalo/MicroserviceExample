using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebMVC.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public bool? FirstLogin { get; set; } = true;

        public int? ContactId { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }

        public DateTime CreationDate { get; set; }

        public bool AdministrationUser { get; set; }
        public bool ContactUser { get; set; }
    }
}
