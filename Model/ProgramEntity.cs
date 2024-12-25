using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class ProgramEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? ProgramID { get; set; }
        public string? Acronym { get; set; }
        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
