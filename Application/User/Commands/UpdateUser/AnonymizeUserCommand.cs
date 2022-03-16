using GhostWriter.Application.Common.Exceptions;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Mappings;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace GhostWriter.Application.User.Commands.UpdateUser
{
    public class AnonymizeUserCommand : IRequest<OutputModel>
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
    }

    public class AnonymizeUserCommandHandler : IRequestHandler<AnonymizeUserCommand, OutputModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;

        public AnonymizeUserCommandHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
        }

        public async Task<OutputModel> Handle(AnonymizeUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManagementFactory.FindUser(request.Username);

            if (user == null)
                throw new NotFoundException($"Author {request.Username} not found.");

            if (!_userManagementFactory.IsInRole(user, request.RoleName))
                throw new AuthorizationException($"User {request.Username} is unauthorized delete the private information.");

            if (_context.Proposals.Where(x => (x.HeadProposal.Ghostwriter.UserName == request.Username || x.HeadProposal.Project.Customer.UserName == request.Username) && x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == Domain.Enums.ProposalStatus.Active).Any())
                throw new Exception($"Cannot anonymize {request.Username} because they have active offers/bids.");

            if (_context.Bookings.Where(x => (x.HeadProposal.Ghostwriter.UserName == request.Username || x.HeadProposal.Project.Customer.UserName == request.Username) && BookingStatusGroups.Open.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)).Any())
                throw new Exception($"Cannot anonymize {request.Username} because they have active project.");

            var roleId = _userManagementFactory.FindRoleIdByName(request.RoleName);
            var ghwInfo = _context.UserRoleDatas.Where(x => x.ApplicationUserRole.UserId == user.Id && x.ApplicationUserRole.RoleId == roleId).FirstOrDefault();
            var userRole = _context.ApplicationUserRoles.Where(x => x.ApplicationUser.Id == user.Id && x.RoleId == roleId).FirstOrDefault();

            try
            {
                if (ghwInfo != null)
                    _context.UserRoleDatas.Remove(ghwInfo);

                if (userRole != null)
                    _context.ApplicationUserRoles.Remove(userRole);

                await _context.SaveChangesAsync(cancellationToken);

                user.BirthDate = null;
                user.EmailConfirmed = false;
                user.FirstName = user.LastName = user.Email = user.NormalizedEmail = user.PhoneNumber = string.Empty;

                _context.ApplicationUsers.Update(user);

                await _context.SaveChangesAsync(cancellationToken);

                return new OutputModel()
                {
                    Success = true,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new OutputModel()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
