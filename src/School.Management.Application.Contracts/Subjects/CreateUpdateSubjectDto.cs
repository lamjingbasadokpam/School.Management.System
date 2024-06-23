using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace School.Management.Subjects
{
    public class CreateUpdateSubjectDto
    {
        public Guid TeacherId { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Class { get; set; }

        
        public Language Languages { get; set; }
    }
}
