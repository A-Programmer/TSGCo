using System;
using System.Linq;
using System.Linq.Expressions;
using Project.Domain.Models.PostKeywordEntities;

namespace Project.Domain.Specifications.PostKeywordSpecifications
{
    public class GetKeywordsByIdsSpecification : Specification<PostKeyword>
    {
        private readonly Guid[] _ids;
        public GetKeywordsByIdsSpecification(Guid[] ids)
        {
            _ids = ids;
        }

        public override Expression<Func<PostKeyword, bool>> ToExpression()
        {
            return i =>
                _ids.Contains(i.Id);
        }
    }
}
