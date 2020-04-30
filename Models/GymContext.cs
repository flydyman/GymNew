using Microsoft.EntityFrameworkCore;

namespace HomeWork.Models
{
    public class GymContext : DbContext
    {
        public DbSet<Client> Clients {get;set;}
        public DbSet<Abonement> Abonements {get;set;}
        public DbSet<Trainer> Trainers {get;set;}
        public DbSet<BasicGroup> BasicGroups {get;set;}
        public DbSet<Training> Trainings {get;set;}
        public DbSet<TrainGroup> TrainGroups {get;set;}

        public GymContext(DbContextOptions<GymContext> options) : base (options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainGroup>().HasKey(x=> new {x.Id_Client, x.Id_Training});
            modelBuilder.Entity<TrainGroup>()
                .HasOne(x=>x.Client)
                .WithMany(x=>x.TrainGroups).HasForeignKey(x=>x.Id_Client);
            modelBuilder.Entity<TrainGroup>()
                .HasOne(x=>x.Training)
                .WithMany(x=>x.TrainGroups).HasForeignKey(x=>x.Id_Training);
        }
    }
}