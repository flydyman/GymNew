using System.Collections.Generic;
using System;
namespace HomeWork.Models.Views
{
    public class FullTrainingInfo
    {
        public int Id {get;set;}
        public DateTime StartAt {get;set;}
        public string TrainerName {get;set;}
        public List<Client> Clients {get;set;}
        public BasicGroup Group {get;set;}
    }
}