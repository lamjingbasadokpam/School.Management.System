using Volo.Abp.Modularity;

namespace School.Management;

[DependsOn(
    typeof(ManagementApplicationModule),
    typeof(ManagementDomainTestModule)
)]
public class ManagementApplicationTestModule : AbpModule
{

}
