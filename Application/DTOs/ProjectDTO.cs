using System;
using AutoMapper;
using System.Collections.Generic;
using GhostWriter.Domain.Enums;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.DTOs
{
    public class ProjectDTO : IMapFrom<Domain.Entities.Project>
    {
        public int Id { get; set; }
        public bool IsPublished { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal MaxBudget { get; set; }
        public decimal CalculatedServiceCharges { get; set; }
        public string ProjectTopic { get; set; }
        public string Description { get; set; }
        public int PagesNo { get; set; }
        public int ProjectStatus { get; set; }
        public int CustomerId { get; set; }
        public string CustomerUsername { get; set; }
        public DegreeDTO MinimumDegreeDTO { get; set; }
        public LanguageDTO LanguageDTO { get; set; }
        public KindOfWorkDTO KindOfWorkDTO { get; set; }
        public List<ExpertiseAreaDTO> ExpertiseAreaListDTOs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Project, ProjectDTO>()
                .ForMember(d => d.CustomerUsername, opt => opt.MapFrom(s => s.Customer.UserName))
                .ForMember(d => d.ProjectStatus, opt => opt.MapFrom(s => (int)s.ProjectStatus))
                .ForMember(d => d.MinimumDegreeDTO, opt => opt.MapFrom(s => s.MinimumDegree))
                .ForMember(d => d.LanguageDTO, opt => opt.MapFrom(s => s.Language))
                .ForMember(d => d.KindOfWorkDTO, opt => opt.MapFrom(s => s.KindOfWork))
                .ForMember(d => d.ExpertiseAreaListDTOs, opt => opt.MapFrom(s => s.ExpertiseAreas));
        }
    }

    public class ProjectDetailsDTO : ProjectDTO
    {
        public List<ProposalInfoDTO> ProposalDetailsDTOs { get; set; }
        public CustomerPublicInfoDTO CustomerPublicInfoDTO { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Project, ProjectDetailsDTO>()
                .ForMember(d => d.CustomerUsername, opt => opt.MapFrom(s => s.Customer.UserName))
                .ForMember(d => d.MinimumDegreeDTO, opt => opt.MapFrom(s => s.MinimumDegree))
                .ForMember(d => d.LanguageDTO, opt => opt.MapFrom(s => s.Language))
                .ForMember(d => d.KindOfWorkDTO, opt => opt.MapFrom(s => s.KindOfWork))
                .ForMember(d => d.ExpertiseAreaListDTOs, opt => opt.MapFrom(s => s.ExpertiseAreas));
        }
    }
}
