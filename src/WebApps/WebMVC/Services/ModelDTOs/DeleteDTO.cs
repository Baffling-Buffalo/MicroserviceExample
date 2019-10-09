using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Services.ModelDTOs
{
    public class DeleteDTO
    {
        public Dictionary<int, string> SuccessfullyDeleted { get; set; }

        public Dictionary<int, string> UnsuccessfullyDeleted { get; set; }
    }
}
