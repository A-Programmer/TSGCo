﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.EntityFrameworkCore;
using Project.Domain.Models.RoleEntities;
using Project.Domain.Models.UserEntities;
using Project.Application.Contracts.Repositories.RoleRepositories;

namespace Project.EntityFrameworkCore.Repositories.RoleRepositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ProjectDbContext db)
            : base(db)
        {
        }

        public async Task<List<User>> GetUsersByRoleId(Guid roleId)
        {
            return await DbContext.Set<User>()
                .Include(x => x.Roles)
                .Where(x => x.Roles.Any(x => x.RoleId == roleId))
                .ToListAsync();
        }

        public async Task<List<Role>> GetRolesByIdsAsync(Guid[] ids)
        {
            return DbContext.Set<Role>().Where(x => ids.Contains(x.Id)).ToList();
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await DbContext.Set<Role>().FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
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
