using School.Management.Permissions;
using School.Management.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace School.Management.Subjects
{
    public class SubjectAppService :
    CrudAppService<
        Subject,
        SubjectDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdateSubjectDto>, 
    ISubjectAppService
    {
        private readonly ITeacherRepository _teacherRepository;
        public SubjectAppService(IRepository<Subject, Guid> repository, ITeacherRepository teacherRepository)
        : base(repository)
        {
            _teacherRepository = teacherRepository;
            GetPolicyName = ManagementPermissions.Subjects.Default;
            GetListPolicyName = ManagementPermissions.Subjects.Default;
            CreatePolicyName = ManagementPermissions.Subjects.Create;
            UpdatePolicyName = ManagementPermissions.Subjects.Edit;
            DeletePolicyName = ManagementPermissions.Subjects.Delete;
        }
        public override async Task<SubjectDto> GetAsync(Guid id)
        {
            
            var queryable = await Repository.GetQueryableAsync();

        
            var query = from subject in queryable
                        join teacher in await _teacherRepository.GetQueryableAsync() on subject.TeacherId equals teacher.Id
                        where subject.Id == id
                        select new { subject, teacher };

           
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Subject), id);
            }

            var subjectDto = ObjectMapper.Map<Subject, SubjectDto>(queryResult.subject);
            subjectDto.TeacherName = queryResult.teacher.Name;
            return subjectDto;
        }

         public override async Task<PagedResultDto<SubjectDto>> GetListAsync(PagedAndSortedResultRequestDto input)
         {
        
            var queryable = await Repository.GetQueryableAsync();

        
            var query = from subject in queryable
                join teacher in await _teacherRepository.GetQueryableAsync() on subject.TeacherId equals teacher.Id
                select new { subject, teacher };

       
            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

           
            var queryResult = await AsyncExecuter.ToListAsync(query);

            
            var subjectdtos = queryResult.Select(x =>
            {
                var subjectdto = ObjectMapper.Map<Subject, SubjectDto>(x.subject);
                subjectdto.TeacherName = x.teacher.Name;
                return subjectdto;
            }).ToList();

           
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<SubjectDto>(
                totalCount,
                subjectdtos
            );
         }

        public async Task<ListResultDto<TeacherLookupDto>> GetTeacherLookupAsync()
        {
            var teachers = await _teacherRepository.GetListAsync();

            return new ListResultDto<TeacherLookupDto>(
                ObjectMapper.Map<List<Teacher>, List<TeacherLookupDto>>(teachers)
            );
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"subject.{nameof(Subject.Name)}";
            }

            if (sorting.Contains("TeacherName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "teahcerName",
                    "teacher.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"teacher.{sorting}";
        }
    }
}
