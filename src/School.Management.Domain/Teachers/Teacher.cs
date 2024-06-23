using School.Management.Genders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace School.Management.Teachers
{
    public class Teacher : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get;  set; }
        public int Age { get; set; }
        public Gender Sex { get; set; }

        private Teacher()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Teacher(
            Guid id,
            string name,
            Gender sex,
            int? age = null)
            : base(id)
        {
            SetName(name);
            Age =(int) age;
            Sex = sex;
        }

        internal Teacher ChangeName(string name)
        {
            SetName(name);
            return this;
        }

        private void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: TeacherConsts.MaxNameLength
            );
        }
    }
}
