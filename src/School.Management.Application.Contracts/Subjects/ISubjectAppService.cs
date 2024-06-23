using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace School.Management.Subjects
{
    public interface ISubjectAppService :
    ICrudAppService< 
        SubjectDto, 
        Guid,
        PagedAndSortedResultRequestDto, 
        CreateUpdateSubjectDto> 
    {
        Task<ListResultDto<TeacherLookupDto>> GetTeacherLookupAsync();
    }
}
