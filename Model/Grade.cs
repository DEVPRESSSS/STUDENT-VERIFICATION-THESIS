using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class Grade
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Prevent auto-generation

        public string? GradeID { get; set; } = string.Empty;

        [Required]
        public decimal? GradeValue { get; set; } 

        [Required]
        public DateTime DateAssigned { get; set; } = DateTime.Now; 

        [Required]
        public string? StudentID { get; set; }

        [ForeignKey("StudentID")]
        public StudentsEntity? Student { get; set; }

        // Foreign Key to the Subject
        [Required]
        public string? SubjectID { get; set; }

        [ForeignKey("SubjectID")]
        public SubjectsEntity? Subject { get; set; }


        [NotMapped]

        public bool isGradeValueLow => GradeValue <75;


        [NotMapped]
        public string? Time { get; set; }

        [NotMapped]
        public string? CourseCode { get; set; }


        [NotMapped]
        public string? ProfessorID { get; set; }


        [NotMapped]
        public string? ProfessorName { get; set; }
    }
}
