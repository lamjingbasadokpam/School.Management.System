using AutoMapper;
using School.Management.Students;
using School.Management.Subjects;
using School.Management.Teachers;

namespace School.Management;

public class ManagementApplicationAutoMapperProfile : Profile
{
    public ManagementApplicationAutoMapperProfile()
    {
        CreateMap<Student, StudentDto>();
        CreateMap<CreateUpdateStudentDto, Student>();

        CreateMap<Subject, SubjectDto>();
        CreateMap<CreateUpdateSubjectDto, Subject>();

        CreateMap<Teacher, TeacherDto>();
        CreateMap<Teacher, TeacherLookupDto>();


    }
}
