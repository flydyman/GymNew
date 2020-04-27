using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using HomeWork.Models;
using HomeWork.Models.Views;

namespace HomeWork.Controllers
{
    public class TrainerController : Controller
    {
        private GymContext db;
        private IConfiguration configuration;
        private int ItemsOnPage;

        public TrainerController(GymContext context, IConfiguration config)
        {
            db = context;
            configuration = config;
            ItemsOnPage = configuration.GetSection("ViewSettings").GetValue<int>("ItemsOnPage");
        }

        public IActionResult Index (int page = 1)
        {
            PagingList<Trainer> model = new PagingList<Trainer>{
                CurrentPage = page,
                PerPage = ItemsOnPage,
                Count = db.Trainers.Count(),
                Items = db.Trainers.OrderBy(x=>x.LastName).OrderBy(x=>x.FirstName)
                    .Skip((page - 1) * ItemsOnPage).Take(ItemsOnPage).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult New ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New (Trainer model)
        {
            db.Trainers.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete (int id)
        {
            db.Trainers.Remove(db.Trainers.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit (int id)
        {
            Trainer model = db.Trainers.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Trainer model)
        {
            db.Trainers.Update(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}