using School.Management.Localization;
using Volo.Abp.AspNetCore.Components;

namespace School.Management.Blazor;

public abstract class ManagementComponentBase : AbpComponentBase
{
    protected ManagementComponentBase()
    {
        LocalizationResource = typeof(ManagementResource);
    }
}
