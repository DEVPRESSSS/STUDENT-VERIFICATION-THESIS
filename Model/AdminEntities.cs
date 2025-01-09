using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class AdminEntities
    {


        [Key]
        public string? AdminID { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]

        public string? Email { get; set; }
        [Required]

        public string? Password { get; set; } = null;
    }
}
