using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Enums;
using GhostWriter.Domain.Entities;

namespace GhostWriter.Application.Lookup.Commands
{
    public class EditCustomFieldCommand : IRequest<OutputModel>
    {
        public object Entity { get; set; }
        public int EntityId { get; set; }
        public FieldStatus FieldStatus { get; set; }

    }
    public class EditCustomFieldCommandHandler : IRequestHandler<EditCustomFieldCommand, OutputModel>
    {
        private readonly IApplicationDbContext _context;

        public EditCustomFieldCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OutputModel> Handle(EditCustomFieldCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Entity.GetType() == typeof(ExpertiseArea))
                {
                    var entity = _context.ExpertiseAreas.Find(request.EntityId);
                    if (entity != null)
                    {
                        entity.FieldStatus = request.FieldStatus;
                        _context.ExpertiseAreas.Update(entity);
                    }
                }

                else if (request.Entity.GetType() == typeof(KindOfWork))
                {
                    var entity = _context.KindOfWorks.Find(request.EntityId);
                    if(entity != null)
                    {
                        entity.FieldStatus = request.FieldStatus;
                        _context.KindOfWorks.Update(entity);
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);

                return new OutputModel()
                {
                    Message = string.Empty,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new OutputModel()
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }
    }
}
