using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationBuilder.Models
{
    public class Choice
    {
        public string Name { get; set; }
        public int CapitalIncrement { get; set; }
        public int OilIncrement { get; set; }
        public int WaterIncrement { get; set; }
        public int FoodIncrement { get; set; }
        public int PopulationIncrement { get; set; }
        public int StabilityIncrement { get; set; }
        public int PrestigeIncrement { get; set; }
        public int CapitalRateIncrement { get; set; }
        public int OilRateIncrement { get; set; }
        public int WaterRateIncrement { get; set; }
        public int FoodRateIncrement { get; set; }
        public int PopulationRateIncrement { get; set; }
        public int StabilityRateIncrement { get; set; }
        public int PrestigeRateIncrement { get; set; }

        public Choice()
        {
            CapitalIncrement = 0;
            OilIncrement = 0;
            WaterIncrement = 0;
            FoodIncrement = 0;
            PopulationIncrement = 0;
            StabilityIncrement = 0;
            PrestigeIncrement = 0;
            CapitalRateIncrement = 0;
            OilRateIncrement = 0;
            WaterRateIncrement = 0;
            FoodRateIncrement = 0;
            PopulationRateIncrement = 0;
            StabilityRateIncrement = 0;
            PrestigeRateIncrement = 0;
        }
    }
}
