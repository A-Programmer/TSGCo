
using System.Linq;
using AutoMapper;
using Project.Auth.Domain.IdentityServer4Entities;

namespace Project.Auth.Mappings
{
    /// <summary>
    /// Defines entity/model mapping for API resources.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class ApiScopeMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="ApiScopeMapperProfile"/>
        /// </summary>
        public ApiScopeMapperProfile()
        {
            // entity to model
            CreateMap<ApiScope, IdentityServer4.Models.ApiScope>(MemberList.Destination)
                .ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.ApiScopeClaims.Select(x => x.Type)));

            // model to entity
            CreateMap<IdentityServer4.Models.ApiScope, ApiScope>(MemberList.Source)
                .ForMember(x => x.ApiScopeClaims, opts => opts.MapFrom(src => src.UserClaims.Select(x => new ApiScopeClaim(x))));
        }
    }
}
