using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeWork.Models.Views
{
    public class NewTraining
    {
        public int Id {get;set;} = 0; // uses only for Training/Edit
        public int Id_Trainer {get;set;}
        public int Id_BaseGroup {get;set;}
        public DateTime StartAt {get;set;}

        public SelectList trainers {get;set;}
        public SelectList groups {get;set;}
    }
}