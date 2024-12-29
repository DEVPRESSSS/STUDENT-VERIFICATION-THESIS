using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class ProfessorsEntity
    {


        [Key]
        public string? ProfessorID { get; set; }
        [Required]

        public string?  Name { get; set; }

        [Required]

        public int Age { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Address { get; set; }
        public string? ProfilePath { get; set; }


        //Foreign key

        public string? DepartmentID { get; set; }    

        [ForeignKey("DepartmentID")]
        public Departments? Departments { get; set; }



        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
