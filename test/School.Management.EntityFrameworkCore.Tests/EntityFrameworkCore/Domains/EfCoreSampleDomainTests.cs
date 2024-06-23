using School.Management.Samples;
using Xunit;

namespace School.Management.EntityFrameworkCore.Domains;

[Collection(ManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ManagementEntityFrameworkCoreTestModule>
{

}
