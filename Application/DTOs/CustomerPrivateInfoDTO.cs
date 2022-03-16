using AutoMapper;
using System.Collections.Generic;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.DTOs
{
    public class CustomerPrivateInfoDTO : CustomerPublicInfoDTO
    {
        public string Email { get; set; }
        public int PagesNoInProgress { get; set; }
        public int PagesWrittenSoFar { get; set; }
        public int NoActiveBids { get; set; }
        public new void Mapping(Profile profile)
        {
            profile.CreateMap<UserRoleData, CustomerPrivateInfoDTO>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.ApplicationUserRole.UserId))
              .ForMember(dest => dest.Username, opt => opt.MapFrom(s => s.ApplicationUserRole.ApplicationUser.UserName))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(s => s.ApplicationUserRole.ApplicationUser.Email));

            profile.CreateMap<CustomerPublicInfoDTO, CustomerPrivateInfoDTO>().ReverseMap();
        }
    }
}
