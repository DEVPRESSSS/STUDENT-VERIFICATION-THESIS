﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class SubjectsEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? SubjectID { get; set; } // Primary key

        [Required]
        public string SubjectName { get; set; } = string.Empty;
        [Required]

        public string? CourseCode { get; set; }
        [Required]

        public int Units { get; set; }

        [Required]
        public string? ProgramID {  get; set; }

        [ForeignKey("ProgramID")]
        public ProgramEntity? Program { get; set; }
        public string? Description { get; set; }


        [Required]

        public string? ProfessorID { get; set; }

        [ForeignKey("ProfessorID")]
        public ProfessorsEntity? Professors  { get; set; }

        // Navigation property for many-to-many relationship with StudentsEntity
        public ICollection<StudentsEntity>? Students { get; set; }


        public ICollection<ScheduleOfSubjects>? Schedules { get; set; }

    }
}
