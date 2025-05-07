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

            CreateMap<Issue, IssueResponseDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.AssignedUser != null ? src.AssignedUser.Name : "N/A"))
                .ForMember(dest => dest.ModifierUserName, opt => opt.MapFrom(src => src.ModifiedBy != null ? src.ModifiedBy.Name : "N/A"))
                .ForMember(dest => dest.ParentIssueId, opt => opt.MapFrom(src => src.ParentIssue.Id))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ToString()))
                .ReverseMap();


            CreateMap<IssueType, IssueTypeDto>();
            CreateMap<Equipment, EquipmentSearchListItemDto>();
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.Authorname, opt => opt.MapFrom(src => src.Author.Name != null ? src.Author.Name : "N/A"));
            CreateMap<Facility, FacilityDto>();

        }
    }
}
