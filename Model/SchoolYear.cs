﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model
{
    public class SchoolYear
    {


        [Required]
        public string? SchoolYearID { get; set; }

        [Required]

        public string? SY { get; set; }

    }
}
