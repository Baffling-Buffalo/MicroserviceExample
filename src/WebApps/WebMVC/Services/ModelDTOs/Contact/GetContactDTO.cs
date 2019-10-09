using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Services.ModelDTOs
{
    public class GetContactDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool Active { get; set; }

        public Dictionary<int, string> Lists { get; set; }

       // public int[] ListIds { get; set; }

     //   public List<string> ListStrings { get; set; }

    }
}
