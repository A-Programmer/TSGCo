using System;
using System.Threading.Tasks;
using Project.Domain.Models.StaticContentEntities;

namespace Project.Application.Contracts.Repositories.StaticContentRepositories
{
    public interface IStaticContentRepository : IRepository<StaticContent>
    {
    }
}
