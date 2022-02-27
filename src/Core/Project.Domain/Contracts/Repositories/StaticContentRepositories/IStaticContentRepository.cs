using System;
using System.Threading.Tasks;
using Project.Domain.Models.StaticContentEntities;

namespace Project.Domain.Contracts.Repositories.StaticContentRepositories
{
    public interface IStaticContentRepository : IRepository<StaticContent>
    {
    }
}
