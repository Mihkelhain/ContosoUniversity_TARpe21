namespace ContosoUniversity_TARpe21.Models
{
    public class Student
    {
        public int ID { get; set; } 
        public string Name { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set;}
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollement { get; set; }
    }
}
