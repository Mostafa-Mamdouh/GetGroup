using API.Dtos;
using AutoMapper;
using GetGroup.API.Dtos;
using GetGroup.Core.Entities;

namespace GetGroup.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserService, UserServiceDto>()
             .ForMember(d => d.UserName, o => o.MapFrom(s => s.AppUser.FirstName + " " + s.AppUser.LastName))
             .ForMember(d => d.ServiceName, o => o.MapFrom(s => s.Service.Name))
             .ForMember(d => d.RequestSatusName, o => o.MapFrom(s => s.RequestSatusId.ToString()));

            CreateMap<Service, ServiceDto>()
             .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
             .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));

        }
    }
}