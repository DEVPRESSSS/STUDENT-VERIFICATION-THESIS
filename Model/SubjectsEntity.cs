using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class SubjectsEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? SubjectID { get; set; } // Primary key

        [Required]
        public string SubjectName { get; set; } = string.Empty;
        [Required]

        public string? CourseCode { get; set; }
        [Required]

        public int Units { get; set; }




        public string?SemesterID { get; set; }

        [ForeignKey("SemesterID")]
        public Semester? Semester { get; set; }

        [Required]
        public string? ProgramID {  get; set; }

        [ForeignKey("ProgramID")]
        public ProgramEntity? Program { get; set; }
        public string? Description { get; set; }



        public string? ProfessorID { get; set; }

        [ForeignKey("ProfessorID")]
        public ProfessorsEntity? Professors { get; set; }


        [Required]

        public string? YearID { get; set; }

        [ForeignKey("YearID")]
        public Year ? Year { get; set; }








        [NotMapped]

        public string? GradeValue { get; set; }


        [NotMapped]
        public bool? IsEnrolled { get; set; } = false;


      


    }
}
