namespace ContosoUniversity_TARpe21.Models
{
    public class InstructorIndexdata
    {
        public IEnumerable< Instructor> Instructors{ get; set; }
        public IEnumerable< Course>Courses { get; set; }
        public IEnumerable< Enrollment>Enrollments { get; set; }
    }
}
