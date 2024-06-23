using Microsoft.AspNetCore.Authorization;
using School.Management.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace School.Management.Teachers
{
    [Authorize(ManagementPermissions.Teachers.Default)]
    public class TeacherAppService : ManagementAppService, ITeacherAppService
    {
        private readonly ITeacherRepository _teacherepository;
        private readonly TeacherManager __teacherManager;

        public TeacherAppService(
            ITeacherRepository teacherRepository,
           TeacherManager teacherManager)
        {
            _teacherepository = teacherRepository;
            __teacherManager = teacherManager;
        }
        public async Task<TeacherDto> GetAsync(Guid id)
        {
            var teacher = await _teacherepository.GetAsync(id);
            return ObjectMapper.Map<Teacher, TeacherDto>(teacher);
        }
        public async Task<PagedResultDto<TeacherDto>> GetListAsync(GetTeacherListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Teacher.Name);
            }

            var teachers = await _teacherepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _teacherepository.CountAsync()
                : await _teacherepository.CountAsync(
                    teacher => teacher.Name.Contains(input.Filter));

            return new PagedResultDto<TeacherDto>(
                totalCount,
                ObjectMapper.Map<List<Teacher>, List<TeacherDto>>(teachers)
            );
        }
        [Authorize(ManagementPermissions.Teachers.Create)]
        public async Task<TeacherDto> CreateAsync(CreateTeacherDto input)
        {
            var teacher = await __teacherManager.CreateAsync(
                input.Name,
                input.Sex,
                input.Age
            );

            await _teacherepository.InsertAsync(teacher);

            return ObjectMapper.Map<Teacher, TeacherDto>(teacher);
        }
        [Authorize(ManagementPermissions.Teachers.Edit)]
        public async Task UpdateAsync(Guid id, UpdateTeacherDto input)
        {
            var teacher = await _teacherepository.GetAsync(id);

            if (teacher.Name != input.Name)
            {
                await __teacherManager.ChangeNameAsync(teacher, input.Name);
            }

            teacher.Sex = input.Sex;
            teacher.Age = input.Age;

            await _teacherepository.UpdateAsync(teacher);
        }
        [Authorize(ManagementPermissions.Teachers.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _teacherepository.DeleteAsync(id);
        }

    }
}
