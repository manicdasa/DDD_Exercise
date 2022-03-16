using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Application.Project.Commands
{
    public class EditProjectStatusCommand : IRequest<OutputModel>
    {
        public int ProjectId { get; set; }
        public int ProjectStatusId { get; set; }
    }

    public class EditProjectStatusHandler : IRequestHandler<EditProjectStatusCommand, OutputModel>
    {
        private readonly IApplicationDbContext _context;

        public EditProjectStatusHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OutputModel> Handle(EditProjectStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Projects.FindAsync(request.ProjectId);

                if(entity == null)
                {
                    return new OutputModel()
                    {
                        Success = false,
                        Message = "Project not found."
                    };
                }

                if (!Enum.IsDefined(typeof(ProjectStatus), request.ProjectStatusId))
                {
                    return new OutputModel()
                    {
                        Success = false,
                        Message = "Invalid project status."
                    };
                }

               // entity.ProjectStatus = (ProjectStatus)request.ProjectStatusId;

                _context.Projects.Update(entity);

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
                    Message = ex.InnerException.Message
                };
            }
        }
    }
}
