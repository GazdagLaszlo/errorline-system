using AutoMapper;
using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.DataContext.Entities;

namespace ErrorlineSystem.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EquipmentOrder, EquipmentOrderDto>()
                .ForPath(dest => dest.State, opt => opt.MapFrom(src => src.State.ToString()));
            CreateMap<EquipmentOrderCreateDto, EquipmentOrder>();
            CreateMap<Issue, IssueDto>();
            CreateMap<Equipment, EquipmentDto>();

            CreateMap<User, UserDto>()
                .ForPath(dest => dest.RoleType, opt => opt.MapFrom(src => src.Role.Type));
            CreateMap<UserCreateDto, User>()
                .ForPath(dest => dest.Role.Type, opt => opt.MapFrom(src => src.RoleType));
        }
    }
}
