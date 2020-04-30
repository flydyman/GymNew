using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeWork.Models.Views
{
    public class DropItems
    {
        public int Id {get;set;}
        public string Name {get;set;}
    }
    public class FullAbonement
    {
        public int Id {get;set;}
        //public string GroupName {get;set;}

        public SelectList Abonements {get;set;}
    }
}