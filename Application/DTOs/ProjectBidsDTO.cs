using System;
using System.Collections.Generic;
using AutoMapper;
using GhostWriter.Application.Common.Mappings;

namespace GhostWriter.Application.DTOs
{
    public class ProjectBidsDTO : IMapFrom<Domain.Entities.Project>
    {
        public int ProjectId { get; set; }
        public string ProjectTopic { get; set; }
        public int PagesNo { get; set; }
        public decimal MaxBudget { get; set; }
        public decimal CalculatedServiceCharges { get; set; }
        public string CustomerUsername { get; set; }
        public LanguageDTO LanguageDTO { get; set; }
        public KindOfWorkDTO KindOfWorkDTO { get; set; }
        public List<ExpertiseAreaDTO> ExpertiseAreaListDTOs { get; set; }
        public int BidsNo { get; set; }
        public ProposalDetails1DTO HighestBid { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Project, ProjectBidsDTO>()
                 .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.Id))
                 .ForMember(d => d.CustomerUsername, opt => opt.MapFrom(s => s.Customer.UserName))
                 .ForMember(d => d.LanguageDTO, opt => opt.MapFrom(s => s.Language))
                 .ForMember(d => d.KindOfWorkDTO, opt => opt.MapFrom(s => s.KindOfWork))
                 .ForMember(d => d.ExpertiseAreaListDTOs, opt => opt.MapFrom(s => s.ExpertiseAreas));
        }
    }
}
