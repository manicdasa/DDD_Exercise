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
    public class AnonymizeUserCheckCommand : IRequest<OutputModel>
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
    }

    public class AnonymizeUserCheckCommandHandler : IRequestHandler<AnonymizeUserCheckCommand, OutputModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;

        public AnonymizeUserCheckCommandHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
        }

        public async Task<OutputModel> Handle(AnonymizeUserCheckCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManagementFactory.FindUser(request.Username);
            var isEligleable = true;
            if (user == null)
                isEligleable = false;

            if (isEligleable && !_userManagementFactory.IsInRole(user, request.RoleName))
                isEligleable = false;

            if (isEligleable && _context.Proposals.Where(x => (x.HeadProposal.Ghostwriter.UserName == request.Username || x.HeadProposal.Project.Customer.UserName == request.Username) && x.ProposalStatuses.OrderByDescending(y => y.DateCreated).FirstOrDefault().ProposalStatus == Domain.Enums.ProposalStatus.Active).Any())
                isEligleable = false;

            if (isEligleable && _context.Bookings.Where(x => (x.HeadProposal.Ghostwriter.UserName == request.Username || x.HeadProposal.Project.Customer.UserName == request.Username) && BookingStatusGroups.Open.Contains(x.BookingStatusHistories.OrderByDescending(y => y.DateCreated).FirstOrDefault().BookingStatus)).Any())
                isEligleable = false;

            return new OutputModel()
            {
                Success = isEligleable,
                Message = string.Empty
            };



        }
    }
}
