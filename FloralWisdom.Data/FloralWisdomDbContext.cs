using Microsoft.EntityFrameworkCore;
using FloralWisdom.Models.Entities;

namespace FloralWisdom.Data
{
    public class FloralWisdomDbContext : DbContext
    {
        public FloralWisdomDbContext()
        { }

        public FloralWisdomDbContext(DbContextOptions<FloralWisdomDbContext> options) 
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<UserPlant> UserPlants { get; set; }
        public DbSet<DiseaseReport> DiseaseReports { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }
        public DbSet<CareReminder> CareReminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserPlant>().HasKey(up => new { up.UserId, up.PlantId });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost, 1433; Database = FloralWisdom; User Id = sa; Password =#AniBonbon128;TrustServerCertificate=True;");
        }
    }
}
