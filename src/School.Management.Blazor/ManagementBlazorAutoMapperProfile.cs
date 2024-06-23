using AutoMapper;
using School.Management.Students;
using School.Management.Subjects;
using School.Management.Teachers;

namespace School.Management.Blazor;

public class ManagementBlazorAutoMapperProfile : Profile
{
    public ManagementBlazorAutoMapperProfile()
    {
        CreateMap<StudentDto, CreateUpdateStudentDto>();

        CreateMap<SubjectDto, CreateUpdateSubjectDto>();

        CreateMap<TeacherDto, UpdateTeacherDto>();

    }
}
