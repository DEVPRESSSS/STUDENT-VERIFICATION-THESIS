
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;


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
        public string? RoleID { get; set; }

        [ForeignKey("RoleID")]
        public Role? Role { get; set; }


        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;



        [NotMapped]

        public string? Character { get; set; }


        [NotMapped]

        public Brush? bgColor { get; set; }

    }
}
