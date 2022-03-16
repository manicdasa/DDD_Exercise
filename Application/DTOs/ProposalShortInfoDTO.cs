using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.DTOs
{
    public class ProposalShortInfoDTO : ProjectShortInfoDTO
    {
        public string ProposalStatus { get; set; }
        public int ProposalId { get; set; }
        public int HeadProposalId { get; set; }
    }
}
