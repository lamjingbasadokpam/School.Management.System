using Volo.Abp.Modularity;

namespace School.Management;

[DependsOn(
    typeof(ManagementDomainModule),
    typeof(ManagementTestBaseModule)
)]
public class ManagementDomainTestModule : AbpModule
{

}
