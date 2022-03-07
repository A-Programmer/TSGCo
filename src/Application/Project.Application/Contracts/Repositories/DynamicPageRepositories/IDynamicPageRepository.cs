using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Models.PageEntities;

namespace Project.Application.Contracts.Repositories.DynamicPageRepositories
{
    public interface IDynamicPageRepository : IRepository<DynamicPage>
    {
    }
}
