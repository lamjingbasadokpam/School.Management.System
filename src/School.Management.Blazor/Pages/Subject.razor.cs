using School.Management.Permissions;
using School.Management.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace School.Management.Blazor.Pages
{
    public partial class Subject
    {
        IReadOnlyList<TeacherLookupDto> teahcerList = Array.Empty<TeacherLookupDto>();
        public Subject() // Constructor
        {
            CreatePolicyName = ManagementPermissions.Subjects.Create;
            UpdatePolicyName = ManagementPermissions.Subjects.Edit;
            DeletePolicyName = ManagementPermissions.Subjects.Delete;
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            teahcerList = (await AppService.GetTeacherLookupAsync()).Items;
        }
        protected override async Task OpenCreateModalAsync()
        {
            if (!teahcerList.Any())
            {
                throw new UserFriendlyException(message: L["ATeacherIsRequiredForCreatingSubject"]);
            }

            await base.OpenCreateModalAsync();
            NewEntity.TeacherId = teahcerList.First().Id;
        }

    }
}
