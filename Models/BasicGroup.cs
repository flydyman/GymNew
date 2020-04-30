using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models
{
    public class BasicGroup
    {
        [Key]
        public int Id {get;set;}
        [Column(TypeName="VARCHAR(100)")]
        public string Description {get;set;}
        public float Costs {get;set;}
        public int MaxClients {get;set;}

        public List<Abonement> Abonements {get;set;}
    }
}