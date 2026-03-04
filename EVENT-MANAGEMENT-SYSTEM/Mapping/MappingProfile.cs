using AutoMapper;
using EVENT_MANAGEMENT_SYSTEM.DTOs;
using EVENT_MANAGEMENT_SYSTEM.Models;

namespace EVENT_MANAGEMENT_SYSTEM.Mapping
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<User,UserResponseDto>().ReverseMap();
        }
    }
}
