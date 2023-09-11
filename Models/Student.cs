using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity_TARpe21.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("First and Last Name")]
        public string FirstMidName { get; set; }

        [Required]
        [DisplayName("Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
    
}

