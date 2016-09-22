using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NationBuilder.Models
{
    [Table("Achievements")]

    public class Achievements
    {
        [Key]
        public int id { get; set; }
        public bool twoHundredOil { get; set; }
        public bool oneHundredMillionPeople { get; set; }
        public bool oneHundredStability { get; set; }
        public bool oneHundredOfEverything { get; set; }
        public bool zeroPopulation { get; set; }
        public bool oneHundredMillionDebt { get; set; }
        public string userId { get; set; }

    }
}
