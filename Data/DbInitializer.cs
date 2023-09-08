using ContosoUniversity_TARpe21.Models;

namespace ContosoUniversity_TARpe21.Data
{
    public class DbInitializer
    {
        public static void Initalize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }
            var students = new Student[]
            {
            new Student() {FirstMidName="Mihkel" ,LastName="Hain", EnrollmentDate=DateTime.Now} ,
            new Student() { FirstMidName = "Hannes", LastName = "Malter", EnrollmentDate = DateTime.Parse("2022-12-13") },
            new Student() { FirstMidName = "Robin", LastName = "Kristanjson", EnrollmentDate = DateTime.Parse("2022-12-13") },
            new Student() { FirstMidName = "Martin", LastName = "Hõbesalu", EnrollmentDate = DateTime.Parse("2022-12-13") },
            new Student() { FirstMidName = "Kristjan ", LastName = "Kivikangur", EnrollmentDate = DateTime.Parse("2022-12-13") },
            new Student() { FirstMidName = "Zilean", LastName = "Gaming", EnrollmentDate = DateTime.Parse("2022-12-13") }
            };
            //context.Students.AddRange(students);
            foreach ( Student s in students ) 
            { 
               context.Students.Add( s );
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course()  {CourseId=1050,Title="Programeerimine",Credits=160},
                new Course()  {CourseId=1240,Title="Keemia",Credits=160},
                new Course()  {CourseId=4302,Title="Heroiin",Credits=160},
                new Course()  {CourseId=7312,Title="LOL 101",Credits=160},
                new Course()  {CourseId=5531,Title="MVC programming",Credits=160},
            };

            foreach (Course c in courses)
            {
                context.Courses.Add( c );
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentId=1,CourseId=1050,grade=Grade.A},
                new Enrollment{StudentId=1,CourseId=1240,grade=Grade.A},
                new Enrollment{StudentId=1,CourseId=4302,grade=Grade.B},
                new Enrollment{StudentId=2,CourseId=5531,grade=Grade.C},
                new Enrollment{StudentId=2,CourseId=1050,grade=Grade.A},
                new Enrollment{StudentId=4,CourseId=1240,grade=Grade.A},
                new Enrollment{StudentId=4,CourseId=4302,grade=Grade.B},
                new Enrollment{StudentId=4,CourseId=5531,grade=Grade.C},
                new Enrollment{StudentId=3,CourseId=1050,grade=Grade.A},
                new Enrollment{StudentId=1,CourseId=1240,grade=Grade.A},
                new Enrollment{StudentId=3,CourseId=4302,grade=Grade.B},
                new Enrollment{StudentId=1,CourseId=5531,grade=Grade.C},
                new Enrollment{StudentId=3,CourseId=1050,grade=Grade.A},
                new Enrollment{StudentId=2,CourseId=1240,grade=Grade.A},
                new Enrollment{StudentId=2,CourseId=4302,grade=Grade.B},
                new Enrollment{StudentId=5,CourseId=5531,grade=Grade.C},
                new Enrollment{StudentId=4,CourseId=1050,grade=Grade.A},

            };

            foreach(Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
        }
    }
