//using AutoMapper;
//using GhostWriter.Application.Common.Interfaces;
//using GhostWriter.Application.DTOs;
//using GhostWriter.Domain.Defaults;
//using GhostWriter.Application.Common.Exceptions;
//using MediatR;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using GhostWriter.Application.Common.Models.Shared;
//using AutoMapper.QueryableExtensions;
//using GhostWriter.Domain.Enums;
//using System;
//using GhostWriter.Application.Common.Mappings;

//namespace GhostWriter.Application.Notification.Queries
//{
//    public class GetUserNotificationsQuery : PaginationModel, IRequest<PagedList<NotificationDTO>>
//    {
//        public string Username { get; set; }
//    }

//    public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, PagedList<NotificationDTO>>
//    {
//        private readonly IApplicationDbContext _context;
//        private readonly IUserManagementFactory _userManagementFactory;
//        private readonly IMapper _mapper;

//        public GetUserNotificationsQueryHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IMapper mapper)
//        {
//            _context = context;
//            _userManagementFactory = userManagementFactory;
//            _mapper = mapper;
//        }

//        public async Task<PagedList<NotificationDTO>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
//        {
//            if (string.IsNullOrWhiteSpace(request.Username))
//                throw new AuthorizationException($"User is unauthorized to get notifications.");

//            var user = await _userManagementFactory.FindUser(request.Username);

//            if (user == null)
//                throw new NotFoundException($"User {request.Username} not found.");

//            try
//            {
//                var query = _context.Notifications.Where(x => x.Receiver.UserName == request.Username).OrderByDescending(x => x.DateTimeCreated)
//                                        .ProjectTo<NotificationDTO>(_mapper.ConfigurationProvider);

//                if (request.Page != default || request.PageSize != default)
//                    return new PagedList<NotificationDTO>(query, request.Page, request.PageSize);
//                else
//                    return new PagedList<NotificationDTO>(query);
//            }
//            catch (Exception ex)
//            {
//                return new PagedList<NotificationDTO>();
//            }
            
//        }
//    }
//}
