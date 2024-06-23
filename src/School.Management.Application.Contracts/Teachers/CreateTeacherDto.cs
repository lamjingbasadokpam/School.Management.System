using School.Management.Genders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace School.Management.Teachers
{
    public class CreateTeacherDto
    {
        [Required]
        [StringLength(TeacherConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public Gender Sex { get; set; }
    }
}
