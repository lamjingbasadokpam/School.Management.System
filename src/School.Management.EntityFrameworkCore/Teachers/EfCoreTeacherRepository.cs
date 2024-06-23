using Microsoft.EntityFrameworkCore;
using School.Management.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace School.Management.Teachers
{
    public class EfCoreTeacherRepository : EfCoreRepository<ManagementDbContext, Teacher, Guid>,
        ITeacherRepository
    {
        public EfCoreTeacherRepository(
        IDbContextProvider<ManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }

        public async Task<Teacher> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(teacher  => teacher.Name == name);
        }

        public async Task<List<Teacher>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    author => author.Name.Contains(filter)
                    )
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
