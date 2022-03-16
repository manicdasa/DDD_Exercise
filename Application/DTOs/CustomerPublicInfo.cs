using AutoMapper;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.DTOs
{
    public class CustomerPublicInfoDTO : IMapFrom<UserRoleData>
    {
        public int Id { get; set; }
        public decimal TotalSpent { get; set; }
        public int JobsPostedCnt { get; set; }
        public string Username { get; set; }
        public bool PaymentVerified { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserRoleData, CustomerPublicInfoDTO>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.ApplicationUserRole.UserId))
              .ForMember(dest => dest.Username, opt => opt.MapFrom(s => s.ApplicationUserRole.ApplicationUser.UserName));
        }
    }
}
