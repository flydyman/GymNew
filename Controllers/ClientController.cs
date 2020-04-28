using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using HomeWork.Models;
using HomeWork.Models.Views;

namespace HomeWork.Controllers
{
    public class ClientController : Controller
    {
        private GymContext db;
        private IConfiguration configuration;
        private int ItemsOnPage;

        public ClientController (GymContext context, IConfiguration config)
        {
            db = context;
            configuration = config;
            ItemsOnPage = configuration.GetSection("ViewSettings").GetValue<int>("ItemsOnPage");
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
                        Abonements = db.Abonements.Where(x=>x.Id_Client == client.Id).Count()
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
    }
}