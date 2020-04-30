using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomeWork.Models;
using HomeWork.Models.Views;

namespace HomeWork.Controllers
{
    public class ClientController : Controller
    {
        private GymContext db;
        private IConfiguration configuration;
        private int ItemsOnPage;
        private int StartWorkAt;
        private int EndWorkAt;

        public ClientController (GymContext context, IConfiguration config)
        {
            db = context;
            configuration = config;
            ItemsOnPage = configuration.GetSection("ViewSettings").GetValue<int>("ItemsOnPage");
            StartWorkAt = configuration.GetSection("WorkingHours").GetValue<int>("Start");
            EndWorkAt = configuration.GetSection("WorkingHours").GetValue<int>("End");
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult SearchResult(string searchString, int page = 1)
        {
            PagingList<Client> model;
            List<Client> c = (
                from client in db.Clients
                where client.LastName.ToLower().StartsWith(searchString.ToLower())
                select client
                ).ToList();
            if (c.Any())
            {
                model = new PagingList<Client>(){
                    CurrentPage = page,
                    PerPage = ItemsOnPage,
                    Count = c.Count /ItemsOnPage,
                    Items = c.Skip((page -1 )* ItemsOnPage).Take(ItemsOnPage).ToList()
                };
            } else {
                model = null;
            }
            ViewBag.search = searchString;
            return View(model);
        }

        public IActionResult Clients (int page=1)
        {
            PagingList<ClientExt> model = new PagingList<ClientExt>{
                CurrentPage = page,
                PerPage = ItemsOnPage,
                Count = db.Clients.Count(),
                Items = (
                    from client in db.Clients
                    select new ClientExt{
                        Id = client.Id,
                        LastName = client.LastName,
                        FirstName = client.FirstName,
                        DateOfBirth = client.DateOfBirth,
                        Gender = client.Gender,
                        AbonementsCount = db.Abonements.Where(x=>x.Id_Client == client.Id).Count()
                    }
                ).ToList()
            };
            
            return View(model);
        }

        [HttpGet]
        public IActionResult New ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New (Client model)
        {
            
                db.Clients.Add(model);
                db.SaveChanges();
            return RedirectToAction("Clients");
        }

        [HttpGet]
        public IActionResult Edit (int id)
        {
            Client model = db.Clients.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit (Client model)
        {
            db.Clients.Update(model);
            db.SaveChanges();
            return RedirectToAction("Clients");
        }

        public IActionResult Delete (int id)
        {
            db.Clients.Remove(db.Clients.Find(id));
            db.SaveChanges();
            return RedirectToAction("Clients");
        }

        public IActionResult Info (int id)
        {
            Client model = db.Clients.Find(id);
            ViewBag.Abonements = 
            (
                from a in db.Abonements
                where a.Client == model
                select a
            ).ToList();
            return View(model);
        }

        public IActionResult Train(int id)
        {
            // Check count of actual abonements
            List<Abonement> abs = db.Abonements.Where(x=> x.Id_Client == id 
                && x.CurrentTrainings > 0
                && x.EndDate.Date.CompareTo(DateTime.UtcNow.Date)>=0
                ).ToList();
            if (abs.Count() == 0) return RedirectToAction("Edit", "Client", new{id=id});
            if (abs.Count() > 1) return ChooseAbo(abs);
            return RedirectToAction("Chooser", "Client", new{model = abs.First()});
        }

        //[HttpGet]
        public IActionResult ChooseAbo(List<Abonement> model)
        {
            /*
            List<FullAbonement> model2 = (
                from a in model
                join g in db.BasicGroups on a.Id_BasicGroup equals g.Id
                join c in db.Clients on a.Id_Client equals c.Id
                select new FullAbonement{
                    Id = a.Id,
                    GroupName = $"{g.Description} until {a.EndDate.ToString("yyyy/mm/dd")}"
                }
            ).ToList();
            */
            // Что тут надо передать:
            //  - ID - привязка
            //  - SelectList - для дропбокса
            //  - хз что ещё

            List<DropItems> drop = (
                from m in model
                join bg in db.BasicGroups on m.Id_BasicGroup equals bg.Id
                select new DropItems {
                    Id = m.Id,
                    Name = $"{bg.Description} - due {m.EndDate.ToShortDateString()} ({m.CurrentTrainings} of {m.TotalTrainings} visits)"
                }
            ).ToList();

            FullAbonement model2 = new FullAbonement{
                Id = 0,
                Abonements = new SelectList(drop, "Id", "Name")
            };
            return View(model2);
        }

/*
        [HttpGet]
        public IActionResult Chooser(int id, DateTime? when)
        {
            
        }
*/
        public IActionResult Assigner(AssignClient model)
        {
            
            model.CalendarPage = (
                from tr in db.Trainings
                join bg in db.BasicGroups on tr.Id_BasicGroup equals bg.Id
                join t in db.Trainers on tr.Id_Trainer equals t.Id
                where tr.StartAt.Date.Equals(model.target.Date)
                select new ShortTrainingInfo{
                    Id = tr.Id,
                    StartAt = tr.StartAt,
                    TrainerName = t.FullName,
                    MaxClients = bg.MaxClients,
                    Id_BasicGroup = bg.Id,
                    Id_Client = model.Id_Client,
                    RegisteredClients = db.TrainGroups.Count(x=>x.Id_Training == tr.Id)
                }
            ).ToList();
            ViewBag.Start = StartWorkAt;
            ViewBag.End = EndWorkAt;
            return View(model);
        }

        [HttpPost]
        public IActionResult Chooser(FullAbonement model)
        {
            //return RedirectToAction("Chooser", "Client", new {id = model.Id, when = DateTime.UtcNow.Date});
            DateTime date = DateTime.UtcNow;
            // Что-то умное будет тут, на часах 23:00 и голова кипит
            // ЖОПА
            //Abonement a = db.Abonements.Find(id);
            AssignClient newModel = (
                from a in db.Abonements
                join c in db.Clients on a.Id_Client equals c.Id
                where a.Id == model.Id
                select new AssignClient{
                    Id_BasicGroup = a.Id_BasicGroup,
                    Id_Client = c.Id,
                    ClientName = $"{c.LastName}, {c.FirstName}",
                    StartDate = (DateTime.UtcNow.Date.CompareTo(a.StartDate.Date)>=0) ?
                        DateTime.UtcNow.Date : a.StartDate.Date,
                    EndDate = a.EndDate
                }
            ).First();
            newModel.target = newModel.StartDate;
            return RedirectToAction("Assigner", "Client", newModel);
        }
    }
}