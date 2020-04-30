using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index(DateTime when)
        {
            Training tr = db.Trainings.First(x => x.StartAt.Equals(when));
            Trainer t = db.Trainers.First(x => x.Id == tr.Id_Trainer);
            List<TrainGroup> tgs = db.TrainGroups.Where(x => x.Id_Training == tr.Id).ToList();
            List<Client> cs = new List<Client>();
            foreach (var item in tgs)
            {
                cs.Add(db.Clients.First(x => x.Id == item.Id_Client));
            }
            BasicGroup bg = db.BasicGroups.First(x => x.Id == tr.Id_BasicGroup);
            FullTrainingInfo model = new FullTrainingInfo{
                Id = tr.Id,
                TrainerName = $"{t.LastName}, {t.FirstName}",
                StartAt = tr.StartAt,
                Clients = cs,
                Group = bg
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult New( DateTime when)
        {
            NewTraining model = new NewTraining{
                StartAt = when,
                trainers = new SelectList(db.Trainers.AsEnumerable(),"Id", "FullName"),
                groups = new SelectList(db.BasicGroups.AsEnumerable(), "Id","Description")
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult New(NewTraining model)
        {
            Training t = new Training{
                Id = 0,
                Id_Trainer = model.Id_Trainer,
                Id_BasicGroup = model.Id_BaseGroup,
                Id_Creator = 0,
                StartAt = model.StartAt
            };
            db.Trainings.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index", "Home", new {date = model.StartAt.Date});
        }

        public async Task<IActionResult> Delete(DateTime when)
        {
            Training t = db.Trainings.First(x=>x.StartAt.Equals(when));
            //db.TrainGroups.RemoveRange(db.TrainGroups.Where(x=>x.Id_Training == t.Id));
            //await db.SaveChangesAsync();
            db.Trainings.Remove(t);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Home", new {date = when.Date});
        }

        [HttpGet]
        public IActionResult Edit (DateTime when)
        {
            Training t = db.Trainings.First(x=>x.StartAt.Equals(when));
            NewTraining model = new NewTraining{
                Id = t.Id,
                Id_Trainer = t.Id_Trainer,
                Id_BaseGroup = t.Id_BasicGroup,
                StartAt = t.StartAt,
                trainers = new SelectList(db.Trainers, "Id", "FullName"),
                groups = new SelectList(db.BasicGroups, "Id", "Description")
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit (NewTraining model)
        {
            Training t = db.Trainings.Find(model.Id);
            t.Id_Trainer = model.Id_Trainer;
            t.Id_BasicGroup = model.Id_BaseGroup;
            db.Trainings.Update(t);
            db.SaveChanges();
            return RedirectToAction("Index", "Home", new{date = model.StartAt.Date});
        }

        public IActionResult Remove (int id, int idClient, DateTime when)
        {
            // Получить абонемент, добавить посещение
            // Удалить связь на эту тренировку, бля
            TrainGroup t = db.TrainGroups.Where(x=>x.Id_Client == idClient && x.Id_Training == id).FirstOrDefault();
            Training tr = db.Trainings.First(x=>x.StartAt.Equals(when));
            Abonement a = db.Abonements.Where(x=>x.Id_Client == idClient && tr.Id_BasicGroup == x.Id_BasicGroup).FirstOrDefault();
            a.CurrentTrainings += 1;
            db.TrainGroups.Remove(t);
            db.Abonements.Update(a);
            db.SaveChanges();
            return RedirectToAction("Index", "Training", new{when = db.Trainings.Find(id).StartAt});
        }

        public IActionResult Assign (DateTime when, int id)
        {
            Training t = db.Trainings.First(x=> x.StartAt.Equals(when));
            
            TrainGroup tg = new TrainGroup{
                Id_Client = id,
                Id_Training = t.Id
            };
            Abonement a = db.Abonements.First(x=>x.Id_Client==id);
            a.CurrentTrainings -= 1;
            db.Abonements.Update(a);
            db.TrainGroups.Add(tg);
            db.SaveChanges();
            return RedirectToAction("Index", "Home", when.Date);
        }
    }
}