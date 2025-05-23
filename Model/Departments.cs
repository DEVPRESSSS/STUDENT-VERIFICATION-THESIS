﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class Departments
    {

        [Key]
        public string? DepartmentID { get; set; }


        [Required]
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
