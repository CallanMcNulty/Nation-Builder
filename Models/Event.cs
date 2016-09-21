using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationBuilder.Models
{
    public class Event
    {
        public string Description { get; set; }
        public List<Choice> Choices { get; set; }

        public Event(string desc, List<Choice> choices)
        {
            Description = desc;
            Choices = choices;
        }
    }
}
