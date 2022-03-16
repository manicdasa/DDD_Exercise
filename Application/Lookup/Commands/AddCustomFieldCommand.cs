using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Entities;
using System.Reflection;

namespace GhostWriter.Application.Lookup.Commands
{
    public class AddCustomFieldCommand : IRequest<OutputModel>
    {
        public string CustomFieldValue { get; set; }
        public object Entity { get; set; }
    }
    public class AddCustomFieldCommandHandler : IRequestHandler<AddCustomFieldCommand, OutputModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly INotificationService _notificationService;

        public AddCustomFieldCommandHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, INotificationService notificationService)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _notificationService = notificationService;
        }

        public async Task<OutputModel> Handle(AddCustomFieldCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var propertyType = request.Entity.GetType();
                var className = $"GhostWriter.Domain.Entities.{propertyType.Name}"; 

                Assembly assem = propertyType.Assembly;
                var customField = assem.CreateInstance(className);

                if (customField is null)
                    return new OutputModel()
                    {
                        Message = "Custom field not found",
                        Success = false
                    };

                PropertyInfo[] properties = customField.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    switch (property.Name)
                    {
                        case "Value":
                            property.SetValue(customField, request.CustomFieldValue, null);
                            break;
                        case "Description":
                            property.SetValue(customField, request.CustomFieldValue, null);
                            break;
                        case "FieldStatus":
                            property.SetValue(customField, 0, null);
                            break;
                        default:
                            break;
                    }
                }

                if(className.Contains(nameof(ExpertiseArea)))
                    _context.ExpertiseAreas.Add((ExpertiseArea)customField);

                else if (className.Contains(nameof(KindOfWork)))
                    _context.KindOfWorks.Add((KindOfWork)customField);

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
