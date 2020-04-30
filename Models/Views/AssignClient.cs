using System.Collections.Generic;
using System;
namespace HomeWork.Models.Views
{
    public class AssignClient
    {
        public int Id_Client{get;set;}
        public string ClientName {get;set;}
        //public int Id_Abonement {get;set;}
        public int Id_BasicGroup {get;set;}
        public List<ShortTrainingInfo> CalendarPage {get;set;}
        public DateTime StartDate {get;set;}
        public DateTime EndDate{get;set;}
        public DateTime target {get;set;}
    }
}