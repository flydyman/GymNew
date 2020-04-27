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
            var i = db.Trainings.Where(x=>
                x.StartAt.Year == currDate.Year && 
                x.StartAt.Month == currDate.Month &&
                x.StartAt.Day == currDate.Day
            ).OrderBy(x=>x.StartAt);

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
                    m.TrainerName = $"{item.trainer.LastName}, {item.trainer.LastName}";
                    m.MaxClients = item.basicGroup.MaxClients;
                    m.RegisteredClients = db.TrainGroups.Count(x=> x.Id_Training == item.Id);
                };
                
            }

            return View(model);
        }

    }
}
