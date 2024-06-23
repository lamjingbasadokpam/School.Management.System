using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace School.Management.Students
{
    public class CreateUpdateStudentDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public string Class { get; set; }

        [Required]
        public int RollNumber { get; set; }
    }
}
