using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class GradeHistory
    {

        [Key]
        public string? HistoryID { get; set; }

        [Required]
        public string? GradeID { get; set; }

        [Required]
        public string? GradeValue { get; set; }

        public DateTime DateAssigned { get; set; }

        [Required]
        public string? StudentID { get; set; }

        [Required]
        public string? SubjectID { get; set; }

        [Required]
        public string? ProfessorName { get; set; }

        [Required]
        public string? EnrollmentID { get; set; }

        [Required]
        public string? StaffID { get; set; }

        [Required]
        public string? SchoolYearID { get; set; }

        public bool isDeleted { get; set; }

        public DateTime ModifiedAt { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; } 

        [ForeignKey("GradeID")]
        public Grade? Grade { get; set; }
    }
}
