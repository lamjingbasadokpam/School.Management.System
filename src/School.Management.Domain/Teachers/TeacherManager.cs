using School.Management.Genders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace School.Management.Teachers
{
    public class TeacherManager : DomainService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherManager(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Teacher> CreateAsync(
            string name,
            Gender sex,
            int? age = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingTeacher = await _teacherRepository.FindByNameAsync(name);
            if (existingTeacher != null)
            {
                throw new TeacherAlreadyExistsException(name);
            }

            return new Teacher(
                GuidGenerator.Create(),
                name,
                sex,
                age
            );
        }

        public async Task ChangeNameAsync(
            Teacher teacher,
            string newName)
        {
            Check.NotNull(teacher, nameof(teacher));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingAuthor = await _teacherRepository.FindByNameAsync(newName);
            if (existingAuthor != null && existingAuthor.Id != teacher.Id)
            {
                throw new TeacherAlreadyExistsException(newName);
            }

            teacher.ChangeName(newName);
        }
    }
}
