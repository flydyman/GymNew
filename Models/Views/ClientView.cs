using System.Collections.Generic;
using System;
namespace HomeWork.Models.Views
{
    public class ClientView
    {
        public string LastName {get;set;}
        public string FirstName {get;set;}
        public Genders Gender {get;set;}
        public DateTime DOB {get;set;}
        public List<Abonement> Abonements {get;set;}
    }
}