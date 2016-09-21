using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NationBuilder.Models
{
    [Table("Log")]
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Entry { get; set; }
        public int NationId { get; set; }
    }
}
