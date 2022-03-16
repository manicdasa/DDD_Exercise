using AutoMapper;
using System.Collections.Generic;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;
using System.Linq;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.DTOs
{
    public class AuthorDTO : IMapFrom<UserRoleData>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool DirectBooking { get; set; }
        public decimal PricePerPage { get; set; }
        public decimal ReviewRating { get; set; }
        public int ReviewCount { get; set; }
        public string PicturePath { get; set; }
        public PictureDTO Picture { get; set; }
        public DegreeDTO HighestDegree { get; set; }
        public List<ExpertiseAreaDTO> ExpertiseAreas { get; set; }
        public List<KindOfWorkDTO> KindOfWorks { get; set; }
        public List<LanguageDTO> Languages { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserRoleData, AuthorDTO>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.ApplicationUserRole.UserId))
              .ForMember(dest => dest.Username, opt => opt.MapFrom(s => s.ApplicationUserRole.ApplicationUser.UserName))
              .ForMember(dest => dest.Picture, opt => opt.MapFrom(s => s.Picture))
              .ForMember(d => d.HighestDegree, opt => opt.MapFrom(s => s.HighestDegree))
              .ForMember(d => d.ExpertiseAreas, opt => opt.MapFrom(s => s.ExpertiseAreas.Where(x => x.FieldStatus != FieldStatus.Rejected)))
              .ForMember(d => d.KindOfWorks, opt => opt.MapFrom(s => s.KindOfWorks.Where(x => x.FieldStatus != FieldStatus.Rejected)))
              .ForMember(d => d.Languages, opt => opt.MapFrom(s => s.Languages));
        }
    }
}
