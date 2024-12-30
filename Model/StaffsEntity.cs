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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        public string? StaffID { get; set; }
        [Required]

        public string? Name { get; set; }
        [Required]

        public string? Email { get; set; }
        [Required]

        public int Contact { get; set; }
        [Required]

        public string? Address { get; set; }


        [Required]
        public string? RoleID { get; set; }

        [ForeignKey("RoleID")]
        public Role? Role { get; set; }


        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

     



    }
}
