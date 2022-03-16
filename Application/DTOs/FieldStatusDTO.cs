using AutoMapper;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.DTOs
{
    public class FieldStatusDTO : IMapFrom<FieldStatus>
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FieldStatus, FieldStatusDTO>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(s => (int)s))
             .ForMember(dest => dest.Value, opt => opt.MapFrom(s => s.ToString()));
        }

    }
}
