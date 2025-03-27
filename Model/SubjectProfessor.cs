using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class SubjectProfessor
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? SubjectProfessorID { get; set; }

        public string? SubjectID { get; set; }

        [ForeignKey("SubjectID")]
        public virtual SubjectsEntity? Subject { get; set; }

        public string? ProfessorID { get; set; }

        [ForeignKey("ProfessorID")]
        public virtual ProfessorsEntity? Professor { get; set; }
        [NotMapped]

        public string? Character { get; set; }


        [NotMapped]

        public Brush? bgColor { get; set; }
    }
}
