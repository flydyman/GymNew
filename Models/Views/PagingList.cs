using System.Collections.Generic;

namespace HomeWork.Models.Views
{
    public class PagingList<T>
    {
        public int CurrentPage {get;set;}
        public int PerPage {get;set;}
        public int Count {get;set;}
        public List <T> Items {get;set;}
    }
}