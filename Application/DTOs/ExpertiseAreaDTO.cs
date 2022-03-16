using AutoMapper;
using System.Collections.Generic;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;
using System.Linq;

namespace GhostWriter.Application.DTOs
{
    public class ExpertiseAreaDTO : IMapFrom<ExpertiseArea>
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public FieldStatusDTO FieldStatusDTO { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExpertiseArea, ExpertiseAreaDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Value))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.FieldStatusDTO, opt => opt.MapFrom(s => s.FieldStatus));

            profile.CreateMap<List<ExpertiseArea>, List<ExpertiseAreaDTO>>();
            profile.CreateMap<IQueryable<ExpertiseArea>, List<ExpertiseAreaDTO>>();
        }
    }
}
