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
                ID_Client = idClient,
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
                Id_Client = model.ID_Client,
                StartDate = model.StartAt,
                EndDate = model.EndAt,
                Id_BasicGroup = model.Id_BasicGroup,
                TotalTrainings = model.TotalTrainings,
                CurrentTrainings = model.TotalTrainings,
                TotalPayed = model.TotalTrainings * db.BasicGroups.Find(model.Id_BasicGroup).Costs
            };
            db.Abonements.Add(a);
            db.SaveChanges();
            return RedirectToAction("Info", "Client", new {id = a.Id_Client});
        }

        public IActionResult Delete (int id)
        {
            Abonement a = db.Abonements.Find(id);
            int c = a.Id_Client;
            db.Abonements.Remove(a);
            db.SaveChanges();
            return RedirectToAction("Info", "Client", new {id = c});
        }
    }
}