using ContosoUniversity_TARpe21.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity_TARpe21.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        { 
        
        }


        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Course>().ToTable("Enrollments");
            modelBuilder.Entity<Course>().ToTable("Studens");
        }
    }
}
