using System;
using AutoMapper;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.DTOs
{
    public class ProjectShortInfoDTO : IMapFrom<Domain.Entities.Project>
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public decimal PlannedBudget { get; set; }
        public decimal MaxBudget { get; set; }
        public string ProjectTopic { get; set; }
        public int PagesNo { get; set; }
        public string CustomerUsername { get; set; }
        public LanguageDTO LanguageDTO { get; set; }
        public KindOfWorkDTO KindOfWorkDTO { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Project, ProjectShortInfoDTO>()
                 .ForMember(d => d.CustomerUsername, opt => opt.MapFrom(s => s.Customer.UserName))
                .ForMember(d => d.LanguageDTO, opt => opt.MapFrom(s => s.Language))
                .ForMember(d => d.KindOfWorkDTO, opt => opt.MapFrom(s => s.KindOfWork));
        }
    }
}
