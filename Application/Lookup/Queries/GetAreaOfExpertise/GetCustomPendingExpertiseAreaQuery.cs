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
    public class GetCustomPendingExpertiseAreaQuery : LookupInputModel, IRequest<PagedList<ExpertiseAreaDTO>>
    {
    }

    public class GetCustomPendingExpertiseAreaQueryHandler : IRequestHandler<GetCustomPendingExpertiseAreaQuery, PagedList<ExpertiseAreaDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomPendingExpertiseAreaQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<ExpertiseAreaDTO>> Handle(GetCustomPendingExpertiseAreaQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ExpertiseAreas.Where(x => x.FieldStatus == Domain.Enums.FieldStatus.Pending).AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Value.ToLower().Contains(request.Search.ToLower()) || x.Description.ToLower().Contains(request.Search.ToLower()));
            }

            var mapped = query.ProjectTo<ExpertiseAreaDTO>(_mapper.ConfigurationProvider);

            IEnumerable<ExpertiseAreaDTO> queryOrdered = mapped;

            if (request.OrderColumn != null && request.OrderColumn.OrderByColumn != null)
            {
                queryOrdered = LinqHelper.OrderByPropertyName<ExpertiseAreaDTO>(mapped, request.OrderColumn.OrderByColumn, request.OrderColumn.OrderByAsc);
            }

            if (request.Page != default || request.PageSize != default)
                return new PagedList<ExpertiseAreaDTO>(queryOrdered, request.Page, request.PageSize);
            else
                return new PagedList<ExpertiseAreaDTO>(queryOrdered);
        }
    }
}