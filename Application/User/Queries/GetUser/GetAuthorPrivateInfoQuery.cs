using AutoMapper;
using GhostWriter.Application.Common.Exceptions;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Application.User.Queries
{
    public class GetAuthorPrivateInfoQuery : IRequest<AuthorPrivateInfoDTO>
    {
       public int GHWId { get; set; }
    }

    public class GetAuthorPrivateInfoQueryHandler : IRequestHandler<GetAuthorPrivateInfoQuery, AuthorPrivateInfoDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IPictureService _pictureService;
        private readonly IMapper _mapper;

        public GetAuthorPrivateInfoQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IPictureService pictureService, IMapper mapper)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _pictureService = pictureService;
            _mapper = mapper;
        }

        public async Task<AuthorPrivateInfoDTO> Handle(GetAuthorPrivateInfoQuery request, CancellationToken cancellationToken)
        {
            var ghw = _userManagementFactory.FindUserById(request.GHWId);

            if (ghw == null)
                throw new NotFoundException($"Author not found.");

            if (!_userManagementFactory.IsInRole(ghw, UserRoleDefaults.GhostwriterRoleName))
                throw new AuthorizationException($"User {ghw.UserName} is unauthorized to access active projects live broadcast.");

            var ghwRoleId = _userManagementFactory.FindRoleIdByName(UserRoleDefaults.GhostwriterRoleName); 

            var ghwData = _context.UserRoleDatas.Where(x => x.ApplicationUserRole.UserId == ghw.Id && x.ApplicationUserRole.RoleId == ghwRoleId).FirstOrDefault();
          
            if (ghwData == null)
                throw new NotFoundException($"Additional information about the author {ghw.UserName} was not found.");

            try
            {
                ghwData.ApplicationUserRole.ApplicationUser = ghw; 

                var retVal = (AuthorPrivateInfoDTO)_mapper.Map(ghwData, typeof(UserRoleData), typeof(AuthorPrivateInfoDTO));

                retVal.PicturePath = (retVal.Picture != null && !string.IsNullOrWhiteSpace(retVal.Picture.LocalPath)) ? (_pictureService.LoadPictureFromFile(new List<string>() { retVal.Picture.LocalPath }, retVal.Picture.PictureFileName, retVal.Picture.MimeType)) : _pictureService.GetDefaultAuthorPicture();

                var ratings = _context.Rates.Where(x => x.Booking.HeadProposal.GHWId == ghw.Id);
                retVal.Ratings = (List<RatingDTO>)_mapper.Map(ratings, typeof(IQueryable<Rate>), typeof(List<RatingDTO>));

                retVal.ReviewRating = retVal.Ratings.Any() ? retVal.Ratings.Sum(x => x.StarRating) / retVal.Ratings.Count() : 0;

                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
