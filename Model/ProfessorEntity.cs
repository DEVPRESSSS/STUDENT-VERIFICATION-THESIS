using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

        public string? Gender { get; set; } 


        public string? ProfilePath { get; set; }


        //Foreign key

        public string? DepartmentID { get; set; }    

        [ForeignKey("DepartmentID")]
        public Departments? Departments { get; set; }



        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]

        public string? Character { get; set; }


        [NotMapped]

        public Brush? bgColor { get; set; }


        //public ICollection<SubjectsEntity>? Subjects { get; set; }


        [NotMapped]


        public ObservableCollection<string> GenderOptions = new ObservableCollection<string>();
        public ProfessorsEntity()
        {

            GenderOptions = new ObservableCollection<string>()
            {
                "Male",
                "Female"


            };


        }

    }
}
