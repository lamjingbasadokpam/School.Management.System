using System.Threading.Tasks;

namespace School.Management.Data;

public interface IManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
