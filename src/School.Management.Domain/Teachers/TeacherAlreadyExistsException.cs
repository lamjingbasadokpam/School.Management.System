using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace School.Management.Teachers
{
    public class TeacherAlreadyExistsException : BusinessException
    {
        public TeacherAlreadyExistsException(string name)
       : base(ManagementDomainErrorCodes.TeacherAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
