using AutoMapper;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GhostWriter.Application.Common.Helpers;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Domain.Enums;

namespace GhostWriter.Application.User.Queries
{
    public class SearchAuthorsQuery : PaginationModel, IRequest<PagedList<AuthorDTO>>
    {
        public int? ProjectId { get; set; }
        public int KindOfWordId { get; set; }
        public int MinimumDegreeId { get; set; }
        public List<int> AreaOfExpertiseIds { get; set; }
        public int LanguageId { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime Deadline { get; set; }
        public decimal PlannedBudget { get; set; }
    }

    public class SearchAuthorsQueryHandler : IRequestHandler<SearchAuthorsQuery, PagedList<AuthorDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;
        private readonly IPictureService _pictureService;
        private readonly IProposalService _proposalService;

        public SearchAuthorsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper, IPictureService pictureService, IProposalService proposalService)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
            _pictureService = pictureService;
            _proposalService = proposalService;
        }

        public async Task<PagedList<AuthorDTO>> Handle(SearchAuthorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var pricePerPage = (request.PlannedBudget != default(decimal) && request.NumberOfPages != default(int)) ? (request.PlannedBudget / request.NumberOfPages) : int.MaxValue;
                var ghwRole = _userManagementFactory.FindRoleByName(UserRoleDefaults.GhostwriterRoleName);
                var degree = _context.Degrees.Find(request.MinimumDegreeId);

                var query = _context.UserRoleDatas.Where(x => 
                                x.ApplicationUserRole.RoleId == ghwRole.Id
                                && x.DirectBooking
                                //&& Math.Round(x.PricePerPage) <= Math.Round(pricePerPage)
                                //&& request.Deadline.AddDays(-1 * (x.PagesPerDay != default(int) ? (request.NumberOfPages / x.PagesPerDay) : 0)).Date >= DateTime.Now.Date
                                && (degree == null || x.HighestDegree.IsHigherOrEqualTo(degree))
                                && x.KindOfWorks.Where(y => y.Id == request.KindOfWordId).Any()
                                && x.Languages.Where(y => y.Id == request.LanguageId).Any());

                if (request.AreaOfExpertiseIds != null && request.AreaOfExpertiseIds.Any())
                {
                    List<int> projectIds = new List<int>();
                    foreach (var exparId in request.AreaOfExpertiseIds ?? new List<int>())
                    {
                        var expertiseArea = _context.ExpertiseAreas.Find(exparId);

                        if (expertiseArea != null)
                        {
                            projectIds.AddRange(query.Where(x => x.ExpertiseAreas.Contains(expertiseArea)).Select(x => x.Id));
                        }
                    }
                    query = query.Where(x => projectIds.Contains(x.Id));
                }

                if (request.ProjectId != null)
                {
                    query = _proposalService.ExcludeActiveProposals(query, (int)request.ProjectId);
                }

                var authors = query
                                .Select(x => new AuthorDTO()
                                {
                                    Id = x.ApplicationUserRole.ApplicationUser.Id,
                                    DirectBooking = x.DirectBooking,
                                    ExpertiseAreas = x.ExpertiseAreas.Where(y => y.FieldStatus != FieldStatus.Rejected).Select(y => new ExpertiseAreaDTO() { Id = y.Id, Description = y.Description, Value = y.Value }).ToList(),
                                    HighestDegree = x.HighestDegree != null ? new DegreeDTO() { Id = x.HighestDegree.Id, Description = x.HighestDegree.Description, Value = x.HighestDegree.Value } : null,
                                    KindOfWorks = x.KindOfWorks.Where(y => y.FieldStatus != FieldStatus.Rejected).Select(y => new KindOfWorkDTO() { Id = y.Id, Description = y.Description, Value = y.Value }).ToList(),
                                    Username = _context.ApplicationUsers.Where(y => y.Id == x.ApplicationUserRole.UserId).FirstOrDefault().UserName,
                                    PicturePath = !string.IsNullOrWhiteSpace(x.Picture.LocalPath) ? _pictureService.LoadPictureFromFile(new List<string>() { x.Picture.LocalPath }, x.Picture.PictureFileName, x.Picture.MimeType) : _pictureService.GetDefaultAuthorPicture(),
                                    PricePerPage = x.PricePerPage,
                                    ReviewRating = _context.Rates.Where(y => y.Booking.HeadProposal.GHWId == x.ApplicationUserRole.ApplicationUser.Id).Any() ? (decimal)Math.Round(_context.Rates.Where(y => y.Booking.HeadProposal.GHWId == x.ApplicationUserRole.ApplicationUser.Id).Average(y => y.StarRating), 1) : 0,
                                    ReviewCount = _context.Rates.Where(y => y.Booking.HeadProposal.GHWId == x.ApplicationUserRole.ApplicationUser.Id).Count()
                                }); 

                if (request.Page != default || request.PageSize != default)
                    return new PagedList<AuthorDTO>(authors, request.Page, request.PageSize);
                else
                    return new PagedList<AuthorDTO>(authors);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
