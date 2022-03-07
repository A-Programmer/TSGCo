using System;
using System.Linq;
using System.Linq.Expressions;
using Project.Domain.Models.CategoryEntities;

namespace Project.Domain.Specifications.PostCategorySpecifications
{
    public class GetCategoriesByIdsSpecification : Specification<PostCategory>
    {
        private readonly Guid[] _ids;
        public GetCategoriesByIdsSpecification(Guid[] ids)
        {
            _ids = ids;
        }

        public override Expression<Func<PostCategory, bool>> ToExpression()
        {
            return i =>
                _ids.Contains(i.Id);
        }
    }
}
