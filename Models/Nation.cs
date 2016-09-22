using BasicAuthentication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NationBuilder.Models
{
    [Table("Nations")]
    public class Nation
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string EconType { get; set; }
        public string GovtType { get; set; }
        public string Geography { get; set; }

        public int Capital { get; set; }
        public int Oil { get; set; }
        public int Water { get; set; }
        public int Food { get; set; }
        public int Population { get; set; }
        public int Stability { get; set; }
        public int Prestige { get; set; }
        public int CapitalRate { get; set; }
        public int OilRate { get; set; }
        public int WaterRate { get; set; }
        public int FoodRate { get; set; }
        public int PopulationRate { get; set; }
        public int StabilityRate { get; set; }
        public int PrestigeRate { get; set; }
        public int Year { get; set; }


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Event GenerateEvent(string action)
        {
            Random random = new Random();
            int RandomEvent = random.Next(0, 100);
            if(Year % 100 == 0)
            {
                Choice c1 = new Choice();
                c1.Name = "Throw an event";
                c1.StabilityIncrement += 60;
                c1.CapitalIncrement -= 20;
                c1.PrestigeRateIncrement += 1;
                Choice c2 = new Choice();
                c2.Name = "Ignore";
                c2.StabilityIncrement += 10;
                return new Event("Centennial anniversary", new List<Choice>() { c1, c2 });
            }
            if (Food == 0)
            {
                Choice c1 = new Choice();
                c1.PopulationIncrement -= 5;
                c1.Name = "Starve";
                return new Event("Famine", new List<Choice>() { c1 });
            }
            if (Water == 0)
            {
                Choice c1 = new Choice();
                c1.PopulationIncrement -= 5;
                c1.Name = "Starve";
                return new Event("Drought", new List<Choice>() { c1 });
            }
            if (Oil == 0)
            {
                Choice c1 = new Choice();
                c1.PopulationIncrement -= 1;
                c1.CapitalIncrement -= 100;
                c1.StabilityIncrement -= 70;
                c1.PrestigeIncrement -= 20;
                c1.Name = "Panic";
                return new Event("Energy Crisis", new List<Choice>() { c1 });
            }
            if (Prestige > 30 && RandomEvent < 10)
            {
                Choice c1 = new Choice();
                c1.Name = "Host the conference";
                c1.OilRateIncrement += 1;
                if (GovtType == "Theocracy")
                {
                    c1.StabilityIncrement -= 10;
                }
                Choice c2 = new Choice();
                c2.Name = "Decline to host the conference";
                if (GovtType == "Theocracy")
                {
                    c2.PrestigeIncrement += 10;
                }
                return new Event("Offer from scientific community to host a conference in your country", new List<Choice>() { c1, c2 });
            }
            if (RandomEvent >= 10 && RandomEvent <= 15 && GovtType == "Theocracy")
            {
                Choice c1 = new Choice();
                c1.Name = "Promote";
                c1.PopulationIncrement += 1;
                Choice c2 = new Choice();
                c2.Name = "Sell artifact to rival nation";
                c2.CapitalIncrement += 10;
                c2.StabilityIncrement -= 5;
                return new Event("Religious artifact uncovered", new List<Choice>() { c1, c2 });
            }
            if (RandomEvent >= 10 && RandomEvent <= 12 && GovtType == "Monarchy")
            {
                Choice c1 = new Choice();
                c1.Name = "New monarch crowned";
                c1.PrestigeIncrement -= 40;
                return new Event("The monarch dies", new List<Choice>() { c1 });
            }
            if (RandomEvent >= 15 && RandomEvent <= 20)
            {
                Choice c1 = new Choice();
                c1.Name = "Make bid";
                c1.PrestigeIncrement += 15;
                c1.CapitalIncrement -= 50;
                Choice c2 = new Choice();
                c2.Name = "Decline";
                c2.PrestigeIncrement -= 20;
                c2.StabilityIncrement -= 10;
                return new Event("Make a bid for the Olympics", new List<Choice>() { c1, c2 });
            }
            if(RandomEvent >= 20 && RandomEvent <= 25 && Stability < 40)
            {
                Choice c1 = new Choice();
                c1.Name = "Carry Out ethnic cleansing";
                c1.PopulationIncrement -= 6;
                c1.StabilityIncrement += 30;
                Choice c2 = new Choice();
                c2.Name = "Indoctrinate the youth";
                c2.CapitalRateIncrement -= 1;
                c2.StabilityRateIncrement += 1;
                c2.StabilityIncrement -= 10;
                Choice c3 = new Choice();
                if (GovtType == "Monarchy")
                {
                    c3.Name = "Public beheadings";
                    c3.PrestigeIncrement -= 20;
                    c3.StabilityIncrement += 15;
                }
                else
                {
                    c3.Name = "Bribe resistance leaders";
                    c3.CapitalIncrement -= 15;
                    c3.StabilityIncrement += 10;
                }
                return new Event("Race Riots", new List<Choice>() { c1, c2, c3 });
            }
            if (RandomEvent < 5 && action != "farm")
            {
                Choice c1 = new Choice();
                c1.FoodIncrement = -5;
                c1.PopulationIncrement = -1;
                c1.Name = "Starve";
                return new Event("Crop failure", new List<Choice>() { c1 });
            }
            if (RandomEvent >=5 && RandomEvent <10 && Stability < 40)
            {
                Choice c1 = new Choice();
                c1.Name = "Acquiesce";
                c1.StabilityIncrement = +10;
                c1.FoodIncrement = -5;
                Choice c2 = new Choice();
                c2.Name = "quash";
                c2.StabilityIncrement = +10;
                c2.PrestigeIncrement = -5;
                return new Event("Bread Riot", new List<Choice>() { c1, c2 });

            }
            if (RandomEvent > 25 && RandomEvent <= 30 && PrestigeRate > 2)
            {
                Choice c1 = new Choice();
                c1.Name = "Limit Civil Liberties";
                c1.PrestigeRateIncrement -= 1;
                c1.StabilityIncrement -= 5;
                c1.StabilityRateIncrement += 1;
                Choice c2 = new Choice();
                c2.Name = "Take military action";
                c2.PrestigeIncrement += 10;
                c2.CapitalIncrement -= 20;
                c2.StabilityIncrement -= 5;
                return new Event("Terrorist Attack", new List<Choice>() { c1, c2 });
            }
            if (RandomEvent >30 && RandomEvent <35)
            {
                Choice c1 = new Choice();
                c1.Name = "rebuild";
                c1.PopulationIncrement -= 2;
                c1.StabilityIncrement -= 3;
                c1.CapitalIncrement -= 5;
                return new Event("Earthquake!", new List<Choice>() { c1 });
            }
            if (RandomEvent >90 && Population > 50 )
            {
                Choice c1 = new Choice();
                c1.Name = "Increase food rations";
                c1.FoodRateIncrement -= 1;
                Choice c2 = new Choice();
                c2.Name = "Ignore demands of your people";
                c2.PopulationIncrement -= 10;
                c2.StabilityIncrement -= 10;
                
                return new Event("Population Increase", new List<Choice>() { c1, c2 });
            }
            if (RandomEvent > 65 && RandomEvent <70 && Geography == "Jungle")
            {
                Choice c1 = new Choice();
                c1.Name = "Burn the dead";
                c1.PopulationIncrement -= 4;
                return new Event("Malaria outbreak", new List<Choice>() { c1 });
            }
            if (RandomEvent > 65 && RandomEvent < 70 && Geography == "Mountain")
            {
                Choice c1 = new Choice();
                c1.Name = "Rebuild";
                c1.PopulationIncrement -= 4;
                return new Event("Landslide", new List<Choice>() { c1 });
            }
            if (RandomEvent > 65 && RandomEvent < 70 && Geography == "Steppe")
            {
                Choice c1 = new Choice();
                c1.Name = "Repel invaders";
                c1.CapitalIncrement -= 4;
                c1.PopulationIncrement -= 4;
                return new Event("Barbarian Invasion", new List<Choice>() { c1 });
            }
            if (RandomEvent > 59 && RandomEvent < 66)
            {
                Choice c1 = new Choice();
                c1.Name = "Rebuild";
                c1.OilIncrement -= (Oil / 2);
                c1.WaterIncrement -= 2;
                return new Event("Sandstorm", new List<Choice>() { c1 });
            }
            
            Choice cx = new Choice();
            cx.Name = "Proceed";
            return new Event("Peace", new List<Choice>() { cx });

        }

        public void ExecuteChoice(Choice choice)
        {
            Capital += choice.CapitalIncrement;
            Oil += choice.OilIncrement;
            Water += choice.WaterIncrement;
            Food += choice.FoodIncrement;
            Population += choice.PopulationIncrement;
            Stability += choice.StabilityIncrement;
            Prestige += choice.PrestigeIncrement;
            CapitalRate += choice.CapitalRateIncrement;
            OilRate += choice.OilRateIncrement;
            WaterRate += choice.WaterRateIncrement;
            FoodRate += choice.FoodRateIncrement;
            PopulationRate += choice.PopulationRateIncrement;
            StabilityRate += choice.StabilityRateIncrement;
            PrestigeRate += choice.PrestigeRateIncrement;
        }

        public void KeepWithinBounds()
        {
            Population = Math.Max(Population, 0);
            Oil = Math.Max(Oil, 0);
            Water = Math.Max(Water, 0);
            Food = Math.Max(Food, 0);
        }
    }
}
