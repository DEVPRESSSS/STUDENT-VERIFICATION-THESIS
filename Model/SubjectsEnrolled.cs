using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public  class SubjectsEnrolled
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? EnrollmentID { get; set; } 

        [Required]
        public string? StudentID { get; set; }

        [ForeignKey("StudentID")]
        public StudentsEntity? Student { get; set; }

        [Required]
        public string? SubjectID { get; set; }

        [ForeignKey("SubjectID")]
        public SubjectsEntity? Subject { get; set; }

        [Required]
        public bool IsEnrolled { get; set; } = false;


        [NotMapped]
        public string? GradeValue { get;set; }


    }
}
