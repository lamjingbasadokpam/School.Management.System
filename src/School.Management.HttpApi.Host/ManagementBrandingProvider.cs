using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace School.Management;

[Dependency(ReplaceServices = true)]
public class ManagementBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Management";
}
