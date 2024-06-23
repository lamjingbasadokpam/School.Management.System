using School.Management.Genders;
using School.Management.Students;
using School.Management.Subjects;
using School.Management.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace School.Management
{
    public class ManagementDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Student, Guid> _studentRepository;
        private readonly IRepository<Subject, Guid> _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly TeacherManager _teacherManager;

        public ManagementDataSeedContributor(IRepository<Student, Guid> studentRepository, IRepository<Subject, Guid> subjectRepository, ITeacherRepository teacherRepository, TeacherManager teacherManager)
        {
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _teacherManager = teacherManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _studentRepository.GetCountAsync() <= 0)
            {
                await _studentRepository.InsertAsync(
                    new Student
                    {
                        Name = "Jingba",
                        Age =15,
                        Class = "5A",
                        RollNumber = 5
                    },
                    autoSave: true
                );

                await _studentRepository.InsertAsync(
                    new Student
                    {
                        Name = "Yuyu",
                        Age = 13,
                        Class = "4A",
                        RollNumber = 18
                    },
                    autoSave: true
                );
            }
            if (await _subjectRepository.GetCountAsync() > 0)
            {
                return;
            }

            var orwell = await _teacherRepository.InsertAsync(
                await _teacherManager.CreateAsync(
                     "George Orwell",
                        Gender.Male,
                        34)
            );

            var douglas = await _teacherRepository.InsertAsync(
                await _teacherManager.CreateAsync(
                  "Douglas Adams",
                        Gender.Male,
                        24
                )
            );

            await _subjectRepository.InsertAsync(
                new Subject
                {
                    TeacherId = orwell.Id, // SET THE AUTHOR
                    Name = "English",
                    Class = "5A",
                    Languages = Language.English
                },
                autoSave: true
            );

            await _subjectRepository.InsertAsync(
                new Subject
                {
                    TeacherId = douglas.Id, // SET THE AUTHOR
                    Name = "General Hindi",
                    Class = "4A",
                    Languages = Language.Hindi
                },
                autoSave: true
            );
            await _subjectRepository.InsertAsync(
                           new Subject
                           {
                               TeacherId = douglas.Id, // SET THE AUTHOR
                               Name = "Maths",
                               Class = "6C",
                               Languages = Language.English
                           },
                           autoSave: true
                       );
        }
    }
}
