using AutoMapper;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.Common.Models.Shared
{
    public class LookupSingleResultModel : IMapFrom<KindOfWork>, IMapFrom<ExpertiseArea>, IMapFrom<Buzzword>
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<KindOfWork, LookupSingleResultModel>()
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Value));

            profile.CreateMap<LookupSingleResultModel, KindOfWork>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Value))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Name));

            profile.CreateMap<ExpertiseArea, LookupSingleResultModel>()
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Value));

            profile.CreateMap<LookupSingleResultModel, ExpertiseArea>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Value))
             .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Name));

            profile.CreateMap<Language, LookupSingleResultModel>()
               .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Value));

            profile.CreateMap<Buzzword, LookupSingleResultModel>()
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Value));

            profile.CreateMap<Degree, LookupSingleResultModel>()
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Value));
        }
    }
}