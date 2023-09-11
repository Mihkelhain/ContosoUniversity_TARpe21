using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity_TARpe21.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal Budget { get; set;}
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}"
            ,ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public int? InstructorId { get; set; }
        [Timestamp]
        public byte RowVersion { get; set; }
        public Instructor Administration { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
