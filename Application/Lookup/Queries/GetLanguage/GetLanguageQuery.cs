using System;
using AutoMapper;
using System.Collections.Generic;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models.Shared;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Helpers;

namespace GhostWriter.Application.Lookup.Queries
{
    public class GetLanguageQuery : LookupInputModel, IRequest<LookupOutputModel>
    {
    }

    public class GetLanguageQueryHandler : IRequestHandler<GetLanguageQuery, LookupOutputModel> 
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLanguageQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LookupOutputModel> Handle(GetLanguageQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Languages.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Value.ToLower().Contains(request.Search.ToLower()));
            }


            if (request.Page != default || request.PageSize != default)
            {
                query = query.Skip(request.Page * request.PageSize).Take(request.PageSize);
            }

            IEnumerable<Language> queryOrdered = query;

            if (request.OrderColumn != null && request.OrderColumn.OrderByColumn != null)
            {
                queryOrdered = LinqHelper.OrderByPropertyName<Language>(query, request.OrderColumn.OrderByColumn, request.OrderColumn.OrderByAsc);
            }

            var sourceList = queryOrdered.ToList();

            try
            {
                var retVal = _mapper.Map(sourceList, typeof(List<Language>), typeof(List<LookupSingleResultModel>));

                return new LookupOutputModel() { Success = true, Message = "", SearchResult = (List<LookupSingleResultModel>)retVal };
            }
            catch(Exception ex)
            {
                return new LookupOutputModel() { Success = false, Message = ex.Message, SearchResult = new List<LookupSingleResultModel>() };
            }
        }
    }
}
