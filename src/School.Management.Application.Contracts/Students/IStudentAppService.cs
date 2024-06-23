using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace School.Management.Students
{
    public interface IStudentAppService :
    ICrudAppService< 
        StudentDto, 
        Guid,
        PagedAndSortedResultRequestDto, 
        CreateUpdateStudentDto> 
    {
    }
}
