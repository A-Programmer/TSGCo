using System;
using System.Linq.Expressions;
using Project.Domain.Models.PostEntities;

namespace Project.Domain.Specifications.PostSpecifications
{
    public class PublicPostSpecification : Specification<Post>
    {
        public override Expression<Func<Post, bool>> ToExpression()
        {
            return i =>
                i.Status;
        }
    }
}
