using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public string Description { get; set; }
        public bool Predefined { get; set; }

        public ApplicationRole() { }

        public ApplicationRole(string name)
        {
            Name = name;
        }
    }
}
