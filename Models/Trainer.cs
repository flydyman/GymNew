using System.ComponentModel.DataAnnotations;
namespace HomeWork.Models
{
    public class Trainer
    {
        [Key]
        public int Id {get;set;}
        public string LastName {get;set;}
        public string FirstName {get;set;}
    }
}