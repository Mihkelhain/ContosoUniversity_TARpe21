using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity_TARpe21.Models
{
    public class OfficeAssignment
    {
        [Key] 
        public int InstructorId { get; set; }
        [StringLength(50)]
        public string location { get; set; }    
        public Instructor Instructor { get; set; }
    }
}
