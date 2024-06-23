using Xunit;

namespace School.Management.EntityFrameworkCore;

[CollectionDefinition(ManagementTestConsts.CollectionDefinitionName)]
public class ManagementEntityFrameworkCoreCollection : ICollectionFixture<ManagementEntityFrameworkCoreFixture>
{

}
