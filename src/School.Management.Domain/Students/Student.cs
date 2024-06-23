using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace School.Management.Students
{
    public class Student : AuditedAggregateRoot<Guid>
    {
        public string Name { get;set;}
        public int Age { get;set;}
        public string Class { get;set;}
        public int RollNumber { get;set;}
    }
}
