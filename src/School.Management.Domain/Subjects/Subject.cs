using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace School.Management.Subjects
{
    public class Subject : AuditedAggregateRoot<Guid>
    {
        public Guid TeacherId { get; set; }
        public string Name {  get; set; }
        public string Class {  get; set; }
        public Language Languages {  get; set; }
    }
}
