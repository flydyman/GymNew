using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace HomeWork.Models
{
    public class Abonement
    {
        [Key]
        public int Id {get;set;}
        public int Id_Client {get;set;}
        [DataType(DataType.Date)]
        public DateTime StartDate {get;set;}
        [DataType(DataType.Date)]
        public DateTime EndDate {get;set;}
        public int TotalTrainings {get;set;}
        public int CurrentTrainings {get;set;}
        public int Id_BasicGroup {get;set;}
        public float TotalPayed {get;set;}
    }
}