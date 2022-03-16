using AutoMapper;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Application.Defaults;
using GhostWriter.Application.DTOs;
using System.Linq;

public class ProposalDetailsDTO : ProposalDTO, IMapFrom<GhostWriter.Domain.Entities.Proposal>
{
    public decimal TaxPercentage { get { return BusinessDefaults.TaxPercentage; } }
    public decimal FeePerPage { get { return BusinessDefaults.FeePerPage; } }
    public string ProposalType { get; set; }
    public string ProposalStatus { get; set; }


    public new void Mapping(Profile profile)
    {
        profile.CreateMap<GhostWriter.Domain.Entities.Proposal, ProposalDetailsDTO>()
             .ForMember(d => d.HeadProposalId, opt => opt.MapFrom(s => s.HeadProposal.Id))
             .ForMember(d => d.ProposalStatus, opt => opt.MapFrom(s => (s.ProposalStatuses != null && s.ProposalStatuses.OrderByDescending(x => x.DateCreated).FirstOrDefault() != null) ? s.ProposalStatuses.OrderByDescending(x => x.DateCreated).FirstOrDefault().ProposalStatus.ToString() : string.Empty))
             .ForMember(d => d.ProposalType, opt => opt.MapFrom(s => s.ProposalType.ToString()))
             .ForMember(d => d.Deadline, opt => opt.MapFrom(s => s.HeadProposal.Project.Deadline))
             .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.HeadProposal.Project.Id))
             .ForMember(d => d.ProjectTopic, opt => opt.MapFrom(s => s.HeadProposal.Project.ProjectTopic))
             .ForMember(d => d.PagesNo, opt => opt.MapFrom(s => s.HeadProposal.Project.PagesNo))
             .ForMember(d => d.CustomerUsername, opt => opt.MapFrom(s => s.HeadProposal.Project.Customer.UserName))
             .ForMember(d => d.AuthorUsername, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.UserName))
            .ForMember(d => d.LanguageDTO, opt => opt.MapFrom(s => s.HeadProposal.Project.Language))
            .ForMember(d => d.KindOfWorkDTO, opt => opt.MapFrom(s => s.HeadProposal.Project.KindOfWork))
            .ForMember(d => d.ExpertiseAreaListDTOs, opt => opt.MapFrom(s => s.HeadProposal.Project.ExpertiseAreas));
    }
}

public class ProposalDetails1DTO : IMapFrom<GhostWriter.Domain.Entities.Proposal>
{
    public int Id { get; set; }
    public int HeadProposalId { get; set; }
    public decimal FinancialOffer { get; set; }
    public decimal ServiceCharges { get; set; }
    public int PagesNo { get; set; }
    public string ProjectTopic { get; set; }
    public int ProjectId { get; set; }
    public string AuthorUsername { get; set; }
    public string AuthorId { get; set; }
    public string ProposalType { get; set; }
    public string ProposalStatus { get; set; }

    public new void Mapping(Profile profile)
    {
        profile.CreateMap<GhostWriter.Domain.Entities.Proposal, ProposalDetails1DTO>()
             .ForMember(d => d.HeadProposalId, opt => opt.MapFrom(s => s.HeadProposal.Id))
             .ForMember(d => d.ProposalStatus, opt => opt.MapFrom(s => (s.ProposalStatuses != null && s.ProposalStatuses.OrderByDescending(x => x.DateCreated).FirstOrDefault() != null) ? s.ProposalStatuses.OrderByDescending(x => x.DateCreated).FirstOrDefault().ProposalStatus.ToString() : string.Empty))
             .ForMember(d => d.ProposalType, opt => opt.MapFrom(s => s.ProposalType.ToString()))
             .ForMember(d => d.PagesNo, opt => opt.MapFrom(s => s.HeadProposal.Project.PagesNo))
             .ForMember(d => d.ProjectTopic, opt => opt.MapFrom(s => s.HeadProposal.Project.ProjectTopic))
             .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.HeadProposal.Project.Id))
             .ForMember(d => d.AuthorUsername, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.UserName))
             .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.HeadProposal.Ghostwriter.Id));
    }
}