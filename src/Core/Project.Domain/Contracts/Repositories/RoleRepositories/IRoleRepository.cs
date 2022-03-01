using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Models.RoleEntities;

namespace Project.Domain.Contracts.Repositories.RoleRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<List<Role>> GetRolesByIdsAsync(Guid[] roleIds);
        Task<Role> GetRoleByNameAsync(string name);
    }
}
