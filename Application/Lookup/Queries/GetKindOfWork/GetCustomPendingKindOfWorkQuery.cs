using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Application.DTOs;
using AutoMapper.QueryableExtensions;
using GhostWriter.Application.Common.Helpers;
using System.Collections.Generic;

namespace GhostWriter.Application.Lookup.Queries
{
    public class GetCustomPendingKindOfWorkQuery : LookupInputModel, IRequest<PagedList<KindOfWorkDTO>>
    {
    }

    public class GetCustomPendingKindOfWorkQueryHandler : IRequestHandler<GetCustomPendingKindOfWorkQuery, PagedList<KindOfWorkDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomPendingKindOfWorkQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<KindOfWorkDTO>> Handle(GetCustomPendingKindOfWorkQuery request, CancellationToken cancellationToken)
        {
            var query = _context.KindOfWorks.Where(x => x.FieldStatus == Domain.Enums.FieldStatus.Pending).AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Value.ToLower().Contains(request.Search.ToLower()) || x.Description.ToLower().Contains(request.Search.ToLower()));
            }

            var mapped = query.ProjectTo<KindOfWorkDTO>(_mapper.ConfigurationProvider);

            IEnumerable< KindOfWorkDTO> queryOrdered = mapped;

            if (request.OrderColumn != null && request.OrderColumn.OrderByColumn != null)
            {
                queryOrdered = LinqHelper.OrderByPropertyName<KindOfWorkDTO>(mapped, request.OrderColumn.OrderByColumn, request.OrderColumn.OrderByAsc);
            }

            if (request.Page != default || request.PageSize != default)
                return new PagedList<KindOfWorkDTO>(queryOrdered, request.Page, request.PageSize);
            else
                return new PagedList<KindOfWorkDTO>(queryOrdered);
        }
    }
}