using AutoMapper;
using HRM.Applicatin;
using HRM.Domain;

namespace HRM.Application.Mappings
{
    public class UserDetailsProfile : Profile
    {
        public UserDetailsProfile()
        {
            CreateMap<UserDetails, UserDetailsDto>()
             .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DepartmentName : null))
             .ForMember(dest => dest.DesignationName, opt => opt.MapFrom(src => src.Designation != null ? src.Designation.DesignationName : null));

            // UserDetails mapping (nested inside User)
            CreateMap<CommandUserDetailsDto, UserDetails>();
        }
    }
}
