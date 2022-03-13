
using System.Linq;
using AutoMapper;
using Project.Auth.Domain.IdentityServer4Entities;

namespace Project.Auth.Mappings
{
    /// <summary>
    /// Defines entity/model mapping for API resources.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class ApiResourceMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="ApiResourceMapperProfile"/>
        /// </summary>
        public ApiResourceMapperProfile()
        {
            // entity to model
            CreateMap<ApiResource, IdentityServer4.Models.ApiResource>(MemberList.Destination)
                .ConstructUsing(src => new IdentityServer4.Models.ApiResource())
                .ForMember(x => x.ApiSecrets, opt => opt.MapFrom(src => src.Secrets.Select(x => x)))
                .ForMember(x => x.Scopes, opt => opt.MapFrom(src => src.Scopes.Select(x => x)))
                .ForMember(x => x.UserClaims, opts => opts.MapFrom(src => src.ApiResourceClaims.Select(x => x.Type)));
            CreateMap<ApiSecret, IdentityServer4.Models.Secret>(MemberList.Destination);
            CreateMap<ApiScope, IdentityServer4.Models.ApiScope>(MemberList.Destination)
                .ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.ApiScopeClaims.Select(x => x.Type)));

            // model to entity
            CreateMap<IdentityServer4.Models.ApiResource, ApiResource>(MemberList.Source)
                .ForMember(x => x.Secrets, opts => opts.MapFrom(src => src.ApiSecrets.Select(x => x)))
                .ForMember(x => x.Scopes, opts => opts.MapFrom(src => src.Scopes.Select(x => x)))
                .ForMember(x => x.ApiResourceClaims, opts => opts.MapFrom(src => src.UserClaims.Select(x => new ApiResourceClaim(x))));
            CreateMap<IdentityServer4.Models.Secret, ApiSecret>(MemberList.Source);
            CreateMap<IdentityServer4.Models.ApiScope, ApiScope>(MemberList.Source)
                .ForMember(x => x.ApiScopeClaims, opts => opts.MapFrom(src => src.UserClaims.Select(x => new ApiScopeClaim(x))));
        }
    }
}
