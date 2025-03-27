using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class SubjectsEnrolled
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
        public string? GradeValue { get; set; }
        [NotMapped]

        public bool? IsSecondSem => Subject?.SemesterID == "SEM102";


        [NotMapped]

        public ObservableCollection<string> Options { get; set; }



        public SubjectsEnrolled()
        {
            Options = new ObservableCollection<string>
                {
                    "FAILED",
                    "INC",
                    "NFE",
                    "NGS",
                    "75",
                    "76",
                    "77",
                    "78",
                    "79",
                    "80",
                    "81",
                    "82",
                    "83",
                    "84",
                    "85",
                    "86",
                    "87",
                    "88",
                    "89",
                    "90",
                    "91",
                    "92",
                    "93",
                    "94",
                    "95",
                    "96",
                    "97",
                    "98",
                    "99",

            };      
          }
    }
       


    
}
