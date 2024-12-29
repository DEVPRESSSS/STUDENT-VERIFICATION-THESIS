using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class StaffsEntity
    {

        [Key]
        public string? StaffID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Contact { get; set; }
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int RoleID { get; set; }

        [ForeignKey("RoleID")]
        public Role? Role { get; set; }



    }
}
