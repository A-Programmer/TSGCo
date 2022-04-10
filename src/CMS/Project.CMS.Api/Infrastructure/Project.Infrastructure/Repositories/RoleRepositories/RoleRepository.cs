using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Contracts.Repositories.RoleRepositories;
using Project.Domain.Models.RoleEntities;
using Project.Domain.Models.UserEntities;

namespace Project.Infrastructure.Repositories.RoleRepositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ProjectDbContext db)
            : base(db)
        {
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return await DbContext.Set<Role>().FirstOrDefaultAsync(x => x.Name.ToLower() == roleName.ToLower());
        }

        public async Task<List<User>> GetUsersByRoleId(Guid roleId)
        {
            return await DbContext.Set<User>()
                .Include(x => x.Roles)
                .Where(x => x.Roles.Any(x => x.Id == roleId))
                .ToListAsync();
        }

        private ProjectDbContext DbContext
        {
            get
            {
                return Context as ProjectDbContext;
            }
        }
    }
}
