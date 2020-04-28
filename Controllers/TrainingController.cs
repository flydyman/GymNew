using System.Xml.Schema;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using HomeWork.Models;
using HomeWork.Models.Views;

namespace HomeWork.Controllers
{
    public class TrainingController : Controller
    {
        private GymContext db;
        private IConfiguration configuration;

        public TrainingController (IConfiguration _config, GymContext context)
        {
            configuration = _config;
            db = context;
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(Training model)
        {
            db.Trainings.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index", "Home", new {date = model.StartAt.Date});
        }
    }
}