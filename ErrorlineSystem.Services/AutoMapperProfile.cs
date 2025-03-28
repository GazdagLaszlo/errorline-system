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
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.ToString()));
            CreateMap<EquipmentOrderCreateDto, EquipmentOrder>();
            CreateMap<Issue, IssueDto>();
            CreateMap<Equipment, EquipmentDto>();
        }
    }
}
