using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace HomeWork.Models
{
    public class TrainGroup
    {
        // 1_to_N
        public int Id_Training {get;set;}
        public int Id_Client {get;set;}

        [ForeignKey("Id_Client")]
        public Client client {get;set;}
        [ForeignKey("Id_Training")]
        public Training training {get;set;}
    }
}