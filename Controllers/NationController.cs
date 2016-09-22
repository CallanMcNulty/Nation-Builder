using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NationBuilder.Models;
using Microsoft.AspNetCore.Identity;
using BasicAuthentication.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections;


namespace NationBuilder.Controllers
{
    public class NationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public NationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string Name, string EconType, string GovtType, string Geography)
        {
            Nation nation = new Nation();
            nation.Name = Name;
            nation.EconType = EconType;
            nation.GovtType = GovtType;
            nation.Geography = Geography;
            nation.Year = 0;

            nation.Capital = 100;
            nation.CapitalRate = 1;
            if(EconType == "Capitalism") { nation.CapitalRate += 1; }
            nation.Oil = 10;
            nation.OilRate = -1;
            if (Geography == "Desert") { nation.OilRate += 2; }
            nation.Water = 10;
            nation.WaterRate = -1;
            if (Geography == "Jungle") { nation.WaterRate += 2; }
            if (EconType == "Capitalism") { nation.Capital -= 5; }
            if (Geography == "Desert") { nation.WaterRate -= -1; }
            nation.Food = 10;
            nation.FoodRate = -1;
            nation.Prestige = 50;
            nation.Prestige = 0;
            if (GovtType == "Anarchy") { nation.Prestige -= 40; }
            if (GovtType == "Monarchy") { nation.PrestigeRate += 1; }
            nation.Stability = 50;
            nation.StabilityRate = 0;
            if (GovtType == "Democracy") { nation.StabilityRate += 1; }
            Random random = new Random();
            if (GovtType == "Anarchy") { nation.Stability = random.Next(0,100); }
            nation.Population = 20;
            nation.PopulationRate = 1;
            if (EconType == "Socialism") { nation.Population += 2; }
            if (Geography == "Megalopolis") { nation.Population += 2; }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            nation.UserId = currentUser.Id;

            _db.Nations.Add(nation);
            _db.SaveChanges();

            return RedirectToAction("Details", new { id = nation.Id });
        }

        public IActionResult Details(int id)
        {
            Nation nation = _db.Nations.FirstOrDefault(n => n.Id == id);
            return View(nation);
        }
        
        public IActionResult ExecuteChoice(int id, Choice choice, string eventName)
        {
            Nation nation = _db.Nations.FirstOrDefault(n => n.Id == id);
            nation.ExecuteChoice(choice);
            nation.KeepWithinBounds();
            Log newLog = new Log();
            newLog.NationId = id;
            newLog.Entry = "In the year " + nation.Year.ToString() +", "+ eventName + " occurred, and the people of "+nation.Name+" chose to " + choice.Name + ".";
            _db.Entry(nation).State = EntityState.Modified;
            _db.Log.Add(newLog);
            _db.SaveChanges();
            return Json(nation);
        }

        public IActionResult AdvanceTurn(int id, string action)
        {
            Nation nation = _db.Nations.FirstOrDefault(n => n.Id == id);
            nation.Capital += nation.CapitalRate;
            nation.Oil += nation.OilRate;
            nation.Prestige += nation.PrestigeRate;
            nation.Stability += nation.StabilityRate;
            nation.Food += nation.FoodRate;
            nation.Water += nation.WaterRate;
            nation.Population += nation.PopulationRate;
            nation.Year++;
            nation.KeepWithinBounds();
            _db.Entry(nation).State = EntityState.Modified;
            _db.SaveChanges();
            Event randomEvent = nation.GenerateEvent(action);
            List<object> result = new List<object>() { nation, randomEvent};
            return Json(result);
        }
        public IActionResult Farm(int id)
        {
            Nation nation = _db.Nations.FirstOrDefault(n => n.Id == id);
            nation.FoodRate += 1;
            nation.WaterRate -= 1;
            _db.Entry(nation).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("AdvanceTurn", new { id = id, action = "farm" });
        }
        public IActionResult SellOil(int id)
        {
            Nation nation = _db.Nations.FirstOrDefault(n => n.Id == id);
            nation.Oil -= 5;
            nation.Capital += 5;
            _db.Entry(nation).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("AdvanceTurn", new { id = id, action = "SellOil" });
        }
        public IActionResult BuildRefinery(int id)
        {
            Nation nation = _db.Nations.FirstOrDefault(n => n.Id == id);
            nation.OilRate += 1;
            nation.Capital -= 10;
            _db.Entry(nation).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("AdvanceTurn", new { id = id, action = "farm" });
        }
        public IActionResult BuildDesalinationPlant(int id)
        {
            Nation nation = _db.Nations.FirstOrDefault(n => n.Id == id);
            nation.OilRate -= 1;
            nation.Capital -= 10;
            nation.WaterRate += 2;
            _db.Entry(nation).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("AdvanceTurn", new { id = id, action = "BuildDesalinationPlant" });
        }
        public IActionResult BuildMonument(int id)
        {
            Nation nation = _db.Nations.FirstOrDefault(n => n.Id == id);
            nation.PrestigeRate += 1;
            nation.Capital -= 20;
            _db.Entry(nation).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("AdvanceTurn", new { id = id, action = "BuildMonument" });
        }
        public IActionResult InvestInPolice(int id)
        {
            Nation nation = _db.Nations.FirstOrDefault(n => n.Id == id);
            nation.StabilityRate += 1;
            nation.CapitalRate -= 1;
            _db.Entry(nation).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("AdvanceTurn", new { id = id, action = "InvestInPolice" });
        }

        [Authorize]
        public IActionResult Log(int id)
        {
            List<Log> r = _db.Log.Where(n => n.NationId == id).ToList();
            return View(r);

        }
        public async Task<IActionResult> AchievementCheck(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            Achievements Achievement = _db.Achievements.FirstOrDefault(n => n.userId == currentUser.Id);
            Nation currentNation = _db.Nations.FirstOrDefault(n => n.Id == id);
            string result = "";
            if(currentNation.Oil >= 200 && !Achievement.twoHundredOil)
            {
                Achievement.twoHundredOil = true;
                result += ", Oil Baron";
            }
            if(currentNation.Population >= 100 && !Achievement.oneHundredMillionPeople)
            {
                Achievement.oneHundredMillionPeople = true;
                result += ", A Decent Reserve";
            }
            if(currentNation.Stability >= 100 && !Achievement.oneHundredStability)
            {
                Achievement.oneHundredStability = true;
                result += ", Like a Rock";
            }
            if(!Achievement.oneHundredOfEverything && currentNation.Year == 100 && currentNation.Population == 100 && currentNation.Prestige == 100 && currentNation.Stability == 100 && currentNation.Capital == 100 && currentNation.Oil == 100 && currentNation.Water == 100 && currentNation.Food == 100)
            {
                Achievement.oneHundredOfEverything = true;
                result += ", 100";
            }
            if(currentNation.Population == 0 && !Achievement.zeroPopulation)
            {
                Achievement.zeroPopulation = true;
                result += ", Genocidal Maniac";
            }
            if(currentNation.Capital <= -100 && !Achievement.oneHundredMillionDebt)
            {
                Achievement.oneHundredMillionDebt = true;
                result += ", Deficit Spender";
            }
            _db.Entry(Achievement).State = EntityState.Modified;
            _db.SaveChanges();
            return Content(result);
        }
     }
}
