using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Form.API.DTO
{
    public class FormDataTableDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string Description { get; set; }
        public string ChkoutFolder { get; set; }
    }
}
