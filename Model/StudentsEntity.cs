using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class StudentsEntity
    {


        public string? Name { get; set; }
        public int Age { get; set; }
        public long IDnumber { get; set; }
        public int Contact { get; set; }
        public string? Gmail { get; set; }
        public string? Address { get; set; }

        public int ProgramID { get; set; }

        [ForeignKey("ProgramID")]
        public ProgramEntity? Program { get; set; }



        public int YearID { get; set; }


        [ForeignKey("YearID")]
        public Year? YearLevel { get; set; }


    }
}
