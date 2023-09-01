namespace ContosoUniversity_TARpe21.Models
{
    public enum Grade
    {
        A,B,C,D,F
    }
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Grade? grade { get; set; }
        public Student student { get; set; }

    }
}

