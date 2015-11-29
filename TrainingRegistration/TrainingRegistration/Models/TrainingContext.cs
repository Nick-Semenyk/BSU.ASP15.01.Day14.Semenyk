using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TrainingRegistration.Models
{
    public class TrainingContext : DbContext
    {
        public DbSet<Training> Trainings { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Student> Students { get; set; }

        public TrainingContext()
        {
            Database.SetInitializer<TrainingContext>(null);
            // Database.SetInitializer(new CreateDatabaseIfNotExists<TrainingContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Training>().HasMany(c => c.Students)
                .WithMany(s => s.Trainings)
                .Map(t => t.MapLeftKey("TrainingId")
                .MapRightKey("StudentId")
                .ToTable("TrainingStudent"));
        }
    }
}