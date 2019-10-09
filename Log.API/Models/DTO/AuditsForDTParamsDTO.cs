using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log.API.Models
{
    public class AuditsForDTParamsDTO
    {
        public IEnumerable<KeyValuePair<string, string>> DataTableParameters { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Update, Create or Delete
        /// </summary>
        public string ActionType { get; set; }
    }
}
