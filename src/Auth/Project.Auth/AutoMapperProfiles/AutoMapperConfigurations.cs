
using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Project.Auth.Areas.Admin.ViewModels;
using Project.Auth.Areas.Admin.ViewModels.Clients;

namespace Project.Auth.AutoMapperProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientViewModel>()
                .ReverseMap();
            
            CreateMap<Client, AddClientViewModel>()
                .ReverseMap();
            
            CreateMap<Client, EditClientViewModel>()
                .ReverseMap();
        }
    }
}