using AutoMapper;
using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Models.Responses;
using BE_BTOnline2.DB;

namespace BE_BTOnline2.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoleRequest, Role>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore()) 
                .ForMember(dest => dest.User, opt => opt.Ignore()); 

            CreateMap<Role, RoleResponse>();

            CreateMap<UserRequest, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<User, UserResponse>();

            CreateMap<AllowAccessRequest, AllowAccess>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<AllowAccess, AllowAccessResponse>();

            CreateMap<InternRequest, Intern>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Intern, InternResponse>();
        }
    }
}
