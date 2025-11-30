using AutoMapper;
using HRM.Applicatin;
using HRM.Domain;

namespace HRM.Application
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<User, QueryUserDto>();

            //CreateMap<CommandUserDto, User>();

            //CreateMap<CommandUserDetailsDto, UserDetails>();


            // User ↔ CommandUserDto
            CreateMap<Users, CommandUserDto>()
                .ReverseMap();

            // UserDetails ↔ CommandUserDetailsDto
            CreateMap<UserDetails, CommandUserDetailsDto>()
                .ReverseMap();

            // User ↔ QueryUserDto
            CreateMap<Users, QueryUserDto>()
                .ReverseMap();


            // User mapping (nested inside Company)
            //CreateMap<CommandUserDto, User>()
            //.ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            //CreateMap<CommandUserDetailsDto, UserDetails>()
            //    .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}
