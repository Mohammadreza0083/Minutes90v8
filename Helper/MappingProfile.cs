using AutoMapper;
using minutes90v8.Dto;
using minutes90v8.Entities;

namespace minutes90v8.Helper
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
