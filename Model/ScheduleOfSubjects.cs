using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class ScheduleOfSubjects
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public string? SchedID { get; set; }


        [Required]

        public string? Time { get; set; }
        [Required]

        public string? SubjectID { get; set; }

        [ForeignKey("SubjectID")]
        public SubjectsEntity? Subject { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
