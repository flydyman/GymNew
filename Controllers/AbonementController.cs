using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using HomeWork.Models;
using HomeWork.Models.Views;

namespace HomeWork.Controllers
{
    public class AbonementController : Controller
    {
        private GymContext db;
        private IConfiguration configuration;

        public AbonementController (IConfiguration _configuration, GymContext context)
        {
            db = context;
            configuration = _configuration;
        }

        [HttpGet]
        public IActionResult New (int idClient)
        {
            AbonementClient model = new AbonementClient{
                client = db.Clients.Find(idClient),
                StartAt = DateTime.UtcNow,
                EndAt = DateTime.UtcNow,
                TotalTrainings = 1,
                basicGroups = db.BasicGroups.ToArray()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult New (AbonementClient model)
        {
            Abonement a = new Abonement{
                client = model.client,
                StartDate = model.StartAt,
                EndDate = model.EndAt,
                basicGroup = model.basicGroup,
                TotalTrainings = model.TotalTrainings,
                CurrentTrainings = model.TotalTrainings,
                TotalPayed = model.TotalTrainings * model.basicGroup.Costs
            };
            db.Abonements.Add(a);
            db.SaveChanges();
            return RedirectToAction("Edit", "Client", new {model.client.Id});
        }

        public IActionResult Delete (int id)
        {
            Abonement a = db.Abonements.Find(id);
            int c = a.client.Id;
            db.Abonements.Remove(a);
            db.SaveChanges();
            return RedirectToAction("Edit", "Client", new {c});
        }
    }
}