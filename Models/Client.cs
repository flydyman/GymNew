using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWork.Models
{
    public enum Genders {
        [Display(Name = "Male")]
        M,
        [Display(Name= "Female")]
        F
    }
    public class Client
    {
        [Key]
        public int Id {get;set;}
        public string LastName {get;set;}
        public string FirstName {get;set;}
        [Column(TypeName="CHAR(1)")]
        public Genders Gender {get;set;}
        [DataType(DataType.Date)]
        public DateTime DateOfBirth {get;set;}

        public List<TrainGroup> TrainGroups {get;set;}
    }
}