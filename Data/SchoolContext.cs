using ContosoUniversity_TARpe21.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");
        }

        public class YourDbContextFactory : IDesignTimeDbContextFactory<SchoolContext>
        {
            public SchoolContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ContsoUniversity;Trusted_Connection=True;MultipleActiveResultSets=True");

                return new SchoolContext(optionsBuilder.Options);
            }
        }
    }
}
