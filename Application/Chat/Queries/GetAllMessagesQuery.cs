using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace GhostWriter.Application.Project.Queries
{
    public class GetAllMessagesQuery : IRequest<List<MessageDTO>>
    {
        public int? HeadProposalId { get; set; }
        public int? BookingId { get; set; }
        public string Username { get; set; }
    }

    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, List<MessageDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IMapper _mapper;

        public GetAllMessagesQueryHandler(IApplicationDbContext context, IMapper mapper, IUserManagementFactory userManagementFactory)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _mapper = mapper;
        }

        public async Task<List<MessageDTO>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new AuthorizationException($"User is unauthorized to access the chat.");

            var user = await _userManagementFactory.FindUser(request.Username);
            bool isAdmin = _userManagementFactory.IsInRole(user, UserRoleDefaults.AdminRoleName);

            if (user == null)
                throw new NotFoundException($"User {request.Username} not found.");

            HeadProposal headProposal = null;
            if (!(request.HeadProposalId is null)) headProposal = _context.HeadProposals.Find((int)request.HeadProposalId);

            Domain.Entities.Booking booking = null;
            if (!(request.BookingId is null)) booking = _context.Bookings.Find((int)request.BookingId);

            if (headProposal is null && booking is null)
                throw new NotFoundException($"Proposal and booking not found.");

            if (headProposal is null)
                headProposal = booking.HeadProposal;

            var customerUsername = headProposal.Project.Customer.UserName;
            var writerUsername = headProposal.Ghostwriter.UserName;

            if (!isAdmin && writerUsername != request.Username && customerUsername != request.Username)
                throw new AuthorizationException($"User is unauthorized to access the chat.");

            var conversation = _context.Conversations.Find(headProposal.Conversation.Id);

            if (conversation == null)
                throw new NotFoundException($"Conversation {headProposal.Conversation.Id} not found.");

            try
            {
                var messages = _context.Messages.Where(x => x.Conversation.Id == conversation.Id).OrderBy(x => x.DateTimeSent)
                 .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider).ToList();
                
                if(!isAdmin)
                    messages.ForEach(x => x.MyMessage = x.Username == request.Username ? true : false);
                else
                    messages.ForEach(x => x.MyMessage = x.Username == customerUsername ? true : false);

                messages.ForEach(x => { x.BookingId = booking is null ? default(int) : booking.Id; });

                return messages;
            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }
            
        }
    }
}
