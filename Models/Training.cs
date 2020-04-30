using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
namespace HomeWork.Models
{
    public class Training
    {
        [Key]
        public int Id {get;set;}
        public int Id_Trainer {get;set;}
        public int Id_BasicGroup {get;set;}
        public int Id_Creator {get;set;} // user, who created this
        [DataType(DataType.DateTime)]
        public DateTime StartAt {get;set;}
        public List<TrainGroup> TrainGroups {get;set;}
    }
}