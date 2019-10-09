using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Form.API.Models
{
    [Table("oz_system")]
    public partial class OzSystem
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("version")]
        [StringLength(127)]
        public string Version { get; set; }
    }
}