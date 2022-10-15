using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Models.RoleEntities;
using Project.Domain.Models.UserEntities;

namespace Project.Domain.Contracts.Repositories.RoleRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetRoleByName(string roleName);
        Task<List<User>> GetUsersByRoleId(Guid roleId);
    }
}
