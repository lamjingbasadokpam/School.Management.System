using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace School.Management.Blazor;

[Dependency(ReplaceServices = true)]
public class ManagementBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Management";
}
