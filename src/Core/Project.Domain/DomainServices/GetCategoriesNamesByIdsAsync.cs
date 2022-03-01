using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Domain.DomainServices
{
    public class PostsManager : IDomainService
    {
        private IUnitOfWork _unitOfWork;
        public PostsManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string[]> GetCategoriesNamesByIdsAsync(Guid[] ids)
        {
            var categories = await _unitOfWork.Categories.GetCategoriesByIds(ids);
            return categories.Select(x => x.Title).ToArray();
        }
    }
}
