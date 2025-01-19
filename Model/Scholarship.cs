using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class Scholarship
    {

        [Key]
        public string? ScholarshipID { get; set; }
        public string? Name { get; set; }
        public string? ScholarshipName { get; set; }
    }
}
