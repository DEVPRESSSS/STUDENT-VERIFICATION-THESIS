using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class Professors
    {


        [Key]
        public string? ProfessorID { get; set; }
        public string?  Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? ProfilePath { get; set; }


        //Foreign key

        public int DepartmentID { get; set; }


        [ForeignKey("DepartmentID")]
        public Departments? Departments { get; set; }
    }
}
