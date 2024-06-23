using School.Management.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace School.Management.Students
{
    public class StudentsAppService :
    CrudAppService<
        Student, //The Book entity
        StudentDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateStudentDto>, //Used to create/update a book
    IStudentAppService
    {
        public StudentsAppService(IRepository<Student, Guid> repository)
        : base(repository)
        {
            GetPolicyName = ManagementPermissions.Students.Default;
            GetListPolicyName = ManagementPermissions.Students.Default;
            CreatePolicyName = ManagementPermissions.Students.Create;
            UpdatePolicyName = ManagementPermissions.Students.Edit;
            DeletePolicyName = ManagementPermissions.Students.Delete;
        }
    }
}
