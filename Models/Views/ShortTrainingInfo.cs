using System;
namespace HomeWork.Models.Views
{
    public class ShortTrainingInfo : IDisposable // For calendar
    {
        public int Id {get;set;}
        public DateTime StartAt {get;set;}
        public string TrainerName {get;set;}
        public int MaxClients {get;set;}
        public int RegisteredClients {get;set;}

        public int Id_BasicGroup {get;set;}
        public int Id_Client {get;set;}

        public void Dispose(){}
    }
}