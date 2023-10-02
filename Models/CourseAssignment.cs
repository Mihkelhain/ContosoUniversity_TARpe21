using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity_TARpe21.Models
{
    public class CourseAssignment
    {
        [Key]
        public int ID { get; set; }

        public int InstructorId { get; set; }

        public int CourseId { get; set; }
        
        public Instructor Instructor { get; set;}
        public Course Course { get; set;}
    }
    
}
