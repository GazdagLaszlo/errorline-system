using AutoMapper;
using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.DataContext.Entities;
using Microsoft.EntityFrameworkCore.Sqlite.Storage.Internal;

namespace ErrorlineSystem.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EquipmentOrder, EquipmentOrderResponseDto>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ToString()))
                .ForMember(dest => dest.EquipmentId, opt => opt.MapFrom(src => src.Equipment.Id))
                .ForMember(dest => dest.IssueId, opt => opt.MapFrom(src => src.Issue.Id));
            CreateMap<EquipmentOrderCreateDto, EquipmentOrder>();
            CreateMap<EquipmentOrder, EquipmentOrderDto>();

            CreateMap<User, UserDto>()
                .ForPath(dest => dest.RoleType, opt => opt.MapFrom(src => src.Role.Type));
            CreateMap<UserCreateDto, User>()
                .ForPath(dest => dest.Role.Type, opt => opt.MapFrom(src => src.RoleType));

            CreateMap<Issue, IssueDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.AssignedUser.Name))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy.Name));
            CreateMap<Equipment, EquipmentDto>();
        }
    }
}
