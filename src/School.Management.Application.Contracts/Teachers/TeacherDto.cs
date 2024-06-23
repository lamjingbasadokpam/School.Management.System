using School.Management.Genders;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace School.Management.Teachers
{
    public class TeacherDto : EntityDto<Guid>
    {
        public string Name { get;  set; }
        public int Age { get; set; }
        public Gender Sex { get; set; }
    }
}
