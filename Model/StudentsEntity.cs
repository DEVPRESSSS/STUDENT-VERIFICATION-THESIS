using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class StudentsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public string StudentID { get; set; } = string.Empty; 

        [Required]
        public string? Name { get; set; }

        [Required]

        public int Age { get; set; }

        [Required]
        public long IDnumber { get; set; }

        [Required]
        public long Contact { get; set; }



        [Required]
        public string? Address { get; set; }


        public string? Picture { get; set; }

        [Required]
        public string ProgramID { get; set; } = string.Empty;

        [ForeignKey("ProgramID")]
        public ProgramEntity? Program { get; set; }

        public string ScholarshipID { get; set; } = string.Empty;

        [ForeignKey("ScholarshipID")]
        public Scholarship? Scholarship { get; set; }

        [Required]
        public string YearID { get; set; } = string.Empty;

        [ForeignKey("YearID")]
        public Year? YearLevel { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]

        public bool IsScholar => ScholarshipID == "SCHO-1001";
        public bool IsScholar4th => ScholarshipID == "SCHO-1001" && YearID== "YearID104";



        [NotMapped]

        public string? Character { get; set; }


        [NotMapped]

        public Brush? bgColor { get; set; }
    }
}
