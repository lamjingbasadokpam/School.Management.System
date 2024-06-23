using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace School.Management.Subjects
{
    public class TeacherLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
