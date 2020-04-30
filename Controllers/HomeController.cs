using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using HomeWork.Models;
using HomeWork.Models.Views;


namespace HomeWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private GymContext db;
        private IConfiguration configuration;

        private int StartWorkAt;
        private int EndWorkAt;

        public HomeController(ILogger<HomeController> logger, GymContext content, IConfiguration config)
        {
            _logger = logger;
            db = content;
            configuration = config;
            StartWorkAt = configuration.GetSection("WorkingHours").GetValue<int>("Start");
            EndWorkAt = configuration.GetSection("WorkingHours").GetValue<int>("End");
        }

        public IActionResult Index(DateTime? date)
        {
            List<ShortTrainingInfo> model = new List<ShortTrainingInfo>();
            DateTime currDate;
            if (date == null)
                currDate = DateTime.UtcNow.Date;
            else currDate = date.GetValueOrDefault();
            /*
            var i = db.Trainings.Where(x=>
                x.StartAt.Year == currDate.Year && 
                x.StartAt.Month == currDate.Month &&
                x.StartAt.Day == currDate.Day
            ).OrderBy(x=>x.StartAt);
            */

            //Console.WriteLine($"Trainings count: {db.Trainings.Count()}");

            model = (
                from tr in db.Trainings
                join bg in db.BasicGroups on tr.Id_BasicGroup equals bg.Id
                join t in db.Trainers on tr.Id_Trainer equals t.Id
                where tr.StartAt.Date.Equals(currDate.Date)
                select new ShortTrainingInfo{
                    Id = tr.Id,
                    StartAt = tr.StartAt,
                    TrainerName = t.FullName,
                    MaxClients = bg.MaxClients,
                    RegisteredClients = db.TrainGroups.Count(x=>x.Id_Training == tr.Id)
                }
            ).ToList();
            /*
            foreach (var item in t1)
            {
                model.Add(
                    new ShortTrainingInfo{
                        StartAt = item.StartAt,
                        TrainerName = item.TrainerName,
                        MaxClients = item.MaxClients,
                        RegisteredClients = db.TrainGroups.Count(x=>x.Id_Training == item.Id)
                    }
                );
            }
            */

            /*
            model = (
                from tr in db.Trainings 
                join bg in db.BasicGroups on tr.Id_BasicGroup equals bg.Id
                join t in db.Trainers on tr.Id_Trainer equals t.Id
                join tg in db.TrainGroups on tr.Id equals tg.Id_Training into v
                from tg in v.DefaultIfEmpty()
                group tg by new {tr.StartAt, t.FullName, bg.MaxClients} into c
                from f in c
                where c.Key.StartAt.Equals(currDate)
                select new ShortTrainingInfo
                {
                    StartAt = c.Key.StartAt,
                    TrainerName = c.Key.FullName,
                    MaxClients = c.Key.MaxClients,
                    RegisteredClients = c.Count() 
                }
                ).ToList();
                */

            /*
            for ( int j = StartWorkAt; j < EndWorkAt; j++)
            {
                model.Add(new ShortTrainingInfo(){
                    StartAt = new DateTime(
                        currDate.Year,
                        currDate.Month,
                        currDate.Day,
                        j,
                        0,
                        0
                    )
                });
            }

            foreach (var item in i)
            {
                using (ShortTrainingInfo m = model.Find(x=>x.StartAt.Hour == item.StartAt.Hour)) 
                {
                    m.StartAt = item.StartAt;
                    m.TrainerName = (await db.Trainers.FindAsync(item.Id_Trainer)).FullName;
                    m.MaxClients = (await db.BasicGroups.FindAsync(item.Id_BasicGroup)).MaxClients;
                    m.RegisteredClients = db.TrainGroups.Count(x=> x.Id_Training == item.Id);
                };
                
            }
            */
            ViewBag.currD = currDate;
            ViewBag.Start = StartWorkAt;
            ViewBag.End = EndWorkAt;

            return View(model);
        }

    }
}
