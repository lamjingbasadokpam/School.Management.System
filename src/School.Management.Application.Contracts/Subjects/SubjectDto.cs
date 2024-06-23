using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace School.Management.Subjects
{
    public class SubjectDto : AuditedEntityDto<Guid>
    {
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public Language Languages { get; set; }
    }
}
