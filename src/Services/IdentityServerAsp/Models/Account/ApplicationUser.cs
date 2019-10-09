using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public bool? FirstLogin { get; set; } = true;

        public int? ContactId { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserGroup> UserGroups { get; set; }

        public DateTime CreationDate { get; set; }

        public bool AdministrationUser { get; set; }
        public bool ContactUser { get; set; }
    }
}
