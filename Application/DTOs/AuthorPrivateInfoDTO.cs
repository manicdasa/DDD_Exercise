using AutoMapper;
using System.Collections.Generic;
using GhostWriter.Domain.Entities;
using System;
using System.Linq;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.DTOs
{
    public class AuthorPublicInfoDTO : AuthorDTO
    {
        public decimal? AvgPricePerPage { get; set; }
        public int PagesPerDay { get; set; }
        public string ProfileIntroduction { get; set; }
        public string Description { get; set; }
        public List<RatingDTO> Ratings { get; set; }

        public new void Mapping(Profile profile)
        {
            profile.CreateMap<UserRoleData, AuthorPublicInfoDTO>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.ApplicationUserRole.UserId))
              .ForMember(d => d.Username, opt => opt.MapFrom(s => s.ApplicationUserRole.ApplicationUser.UserName))
              .ForMember(d => d.HighestDegree, opt => opt.MapFrom(s => s.HighestDegree))
              .ForMember(d => d.ExpertiseAreas, opt => opt.MapFrom(s => s.ExpertiseAreas.Where(x => x.FieldStatus != FieldStatus.Rejected)))
              .ForMember(d => d.KindOfWorks, opt => opt.MapFrom(s => s.KindOfWorks.Where(x => x.FieldStatus != FieldStatus.Rejected)));
        }
    }

    public class AuthorPrivateInfoDTO : AuthorPublicInfoDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public new void Mapping(Profile profile)
        {
            profile.CreateMap<UserRoleData, AuthorPrivateInfoDTO>()
              .ForMember(d => d.Email, opt => opt.MapFrom(s => s.ApplicationUserRole.ApplicationUser.Email))
              .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.ApplicationUserRole.ApplicationUser.FirstName))
              .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.ApplicationUserRole.ApplicationUser.LastName))
              .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.ApplicationUserRole.UserId))
              .ForMember(d => d.Username, opt => opt.MapFrom(s => s.ApplicationUserRole.ApplicationUser.UserName))
              .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.ApplicationUserRole.ApplicationUser.BirthDate))
              .ForMember(d => d.HighestDegree, opt => opt.MapFrom(s => s.HighestDegree))
              .ForMember(d => d.ExpertiseAreas, opt => opt.MapFrom(s => s.ExpertiseAreas.Where(x => x.FieldStatus != FieldStatus.Rejected)))
              .ForMember(d => d.KindOfWorks, opt => opt.MapFrom(s => s.KindOfWorks.Where(x => x.FieldStatus != FieldStatus.Rejected)));

            profile.CreateMap< AuthorPrivateInfoDTO, AuthorPublicInfoDTO>()
              .ForMember(d => d.ExpertiseAreas, opt => opt.MapFrom(s => s.ExpertiseAreas.Where(x => x.FieldStatusDTO.Id != (int)FieldStatus.Rejected)))
              .ForMember(d => d.KindOfWorks, opt => opt.MapFrom(s => s.KindOfWorks.Where(x => x.FieldStatusDTO.Id != (int)FieldStatus.Rejected)));

            profile.CreateMap< AuthorPublicInfoDTO, AuthorPrivateInfoDTO>()
                .ForMember(d => d.ExpertiseAreas, opt => opt.MapFrom(s => s.ExpertiseAreas.Where(x => x.FieldStatusDTO.Id != (int)FieldStatus.Rejected)))
                .ForMember(d => d.KindOfWorks, opt => opt.MapFrom(s => s.KindOfWorks.Where(x => x.FieldStatusDTO.Id != (int)FieldStatus.Rejected)));
        }
    }
}
