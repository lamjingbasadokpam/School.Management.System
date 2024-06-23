using School.Management.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace School.Management.Permissions;

public class ManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var managementGroup = context.AddGroup(ManagementPermissions.GroupName, L("Permission:Management"));

        var studentPermission = managementGroup.AddPermission(ManagementPermissions.Students.Default, L("Permission:Students"));
        studentPermission.AddChild(ManagementPermissions.Students.Create, L("Permission:Students.Create"));
        studentPermission.AddChild(ManagementPermissions.Students.Edit, L("Permission:Students.Edit"));
        studentPermission.AddChild(ManagementPermissions.Students.Delete, L("Permission:Students.Delete"));

        var subjectPermission = managementGroup.AddPermission(ManagementPermissions.Subjects.Default, L("Permission:Subjects"));
        subjectPermission.AddChild(ManagementPermissions.Subjects.Create, L("Permission:Subjects.Create"));
        subjectPermission.AddChild(ManagementPermissions.Subjects.Edit, L("Permission:Subjects.Edit"));
        subjectPermission.AddChild(ManagementPermissions.Subjects.Delete, L("Permission:Subjects.Delete"));

        var teacherPermission = managementGroup.AddPermission(ManagementPermissions.Teachers.Default, L("Permission:Teachers"));
        teacherPermission.AddChild(ManagementPermissions.Teachers.Create, L("Permission:Teachers.Create"));
        teacherPermission.AddChild(ManagementPermissions.Teachers.Edit, L("Permission:Teachers.Edit"));
        teacherPermission.AddChild(ManagementPermissions.Teachers.Delete, L("Permission:Teachers.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ManagementResource>(name);
    }
}
