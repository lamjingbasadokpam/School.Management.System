using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace School.Management.Students
{
    public class StudentDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Class { get; set; }
        public int RollNumber { get; set; }
    }
}
