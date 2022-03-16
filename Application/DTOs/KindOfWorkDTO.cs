using AutoMapper;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.DTOs
{
    public class KindOfWorkDTO : IMapFrom<KindOfWork>
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public FieldStatusDTO FieldStatusDTO { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<KindOfWork, KindOfWorkDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Value))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.FieldStatusDTO, opt => opt.MapFrom(s => s.FieldStatus));
        }
    }
}
