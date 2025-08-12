using AutoMapper;
using minutes90.Dto;
using minutes90.Entities;

namespace minutes90.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUsers, UserDto>();
            CreateMap<RegisterDto, AppUsers>();
        }
    }
}
