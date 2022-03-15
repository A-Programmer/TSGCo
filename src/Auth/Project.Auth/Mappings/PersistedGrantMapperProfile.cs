﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AutoMapper;
using Project.Auth.Domain.IdentityServer4Entities;

namespace Project.Auth.Mappings
{
    /// <summary>
    /// Defines entity/model mapping for persisted grants.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class PersistedGrantMapperProfile:Profile
    {
        /// <summary>
        /// <see cref="PersistedGrantMapperProfile">
        /// </see>
        /// </summary>
        public PersistedGrantMapperProfile()
        {
            // entity to model
            CreateMap<PersistedGrant, IdentityServer4.Models.PersistedGrant>(MemberList.Destination)
                .ForMember(x => x.SubjectId, opt => opt.MapFrom(x => x.UserId));

            // model to entity
            CreateMap<IdentityServer4.Models.PersistedGrant, PersistedGrant>(MemberList.Source)
            .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.SubjectId));
        }
    }
}
