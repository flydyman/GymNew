using System.ComponentModel.DataAnnotations;
using System;

namespace HomeWork.Models.Views
{
    public class AbonementClient
    {
        public Client client {get;set;}
        public int ID_Client {get;set;}
        [DataType(DataType.Date)]
        public DateTime StartAt {get;set;}
        [DataType(DataType.Date)]
        public DateTime EndAt {get;set;}
        public int TotalTrainings {get;set;}
        public int Id_BasicGroup {get;set;}
        public BasicGroup[] basicGroups {get;set;}
    }
}