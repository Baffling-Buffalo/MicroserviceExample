using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.DTO
{
    public class LockUsersDTO
    {
        public DateTime? LockUntil { get; set; } = null;
        public string[] UserIds { get; set; }
    }
}
