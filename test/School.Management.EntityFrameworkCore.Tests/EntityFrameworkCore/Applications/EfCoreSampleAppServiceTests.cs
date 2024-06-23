using School.Management.Samples;
using Xunit;

namespace School.Management.EntityFrameworkCore.Applications;

[Collection(ManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ManagementEntityFrameworkCoreTestModule>
{

}
