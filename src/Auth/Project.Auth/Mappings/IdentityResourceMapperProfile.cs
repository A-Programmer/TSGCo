﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Linq;
using AutoMapper;
using Project.Auth.Domain.IdentityServer4Entities;

namespace Project.Auth.Mappings
{
    /// <summary>
    /// Defines entity/model mapping for identity resources.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class IdentityResourceMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="IdentityResourceMapperProfile"/>
        /// </summary>
        public IdentityResourceMapperProfile()
        {
            // entity to model
            CreateMap<IdentityResource, IdentityServer4.Models.IdentityResource>(MemberList.Destination)
                .ConstructUsing(src => new IdentityServer4.Models.IdentityResource())
                .ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.IdentityClaims.Select(x => x.Type)));

            // model to entity
            CreateMap<IdentityServer4.Models.IdentityResource, IdentityResource>(MemberList.Source)
                .ForMember(x => x.IdentityClaims, opts => opts.MapFrom(src => src.UserClaims.Select(x => new IdentityClaim(x))));
        }
    }
}
