using Volo.Abp.Modularity;

namespace School.Management;

public abstract class ManagementApplicationTestBase<TStartupModule> : ManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
