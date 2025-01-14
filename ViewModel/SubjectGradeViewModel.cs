using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class SubjectGradeViewModel
    {

        public string? SubjectID { get; set; }
        public string? SubjectName { get; set; }
        public decimal? GradeValue { get; set; } = 0;
        public string? GradeID { get; set; } 
    }
}
