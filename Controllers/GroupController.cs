using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using HomeWork.Models;
using HomeWork.Models.Views;

namespace HomeWork.Controllers
{
    public class GroupController : Controller
    {
        private GymContext db;
        private IConfiguration configuration;
        private int ItemsOnPage;

        public GroupController (GymContext context, IConfiguration config)
        {
            db = context;
            configuration = config;
            ItemsOnPage = configuration.GetSection("ViewSettings").GetValue<int>("ItemsOnPage");
        }

        public IActionResult Index (int page = 1)
        {
            PagingList<BasicGroup> model = new PagingList<BasicGroup>{
                CurrentPage = page,
                PerPage = ItemsOnPage,
                Count = db.BasicGroups.Count(),
                Items = db.BasicGroups.OrderBy(x=>x.Id)
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
        public IActionResult New( BasicGroup model)
        {
            db.BasicGroups.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit (int id)
        {
            BasicGroup model = db.BasicGroups.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit (BasicGroup model)
        {
            db.BasicGroups.Update(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete (int id)
        {
            db.BasicGroups.Remove(db.BasicGroups.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}