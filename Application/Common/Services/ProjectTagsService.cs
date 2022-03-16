using GhostWriter.Application.Common.Exceptions;
using GhostWriter.Application.Common.Helpers;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Entities;
using GhostWriter.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using GhostWriter.Application.Common.Models.Shared;
using System.Linq;
using GhostWriter.Domain.Defaults;

namespace GhostWriter.Application.Common.Services
{
    public class ProjectTagsService : IProjectTagsService
    {
        private readonly IApplicationDbContext _context;

        public ProjectTagsService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> AddCustomField(string CustomFieldValue, object Entity, CancellationToken cancellationToken)
        {
            try
            {
                var propertyType = Entity.GetType();
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
                            property.SetValue(customField, CustomFieldValue, null);
                            break;
                        case "Description":
                            property.SetValue(customField, CustomFieldValue, null);
                            break;
                        case "FieldStatus":
                            property.SetValue(customField, 0, null);
                            break;
                        default:
                            break;
                    }
                }

                if (className.Contains(nameof(ExpertiseArea)))
                    _context.ExpertiseAreas.Add((ExpertiseArea)customField);

                else if (className.Contains(nameof(KindOfWork)))
                    _context.KindOfWorks.Add((KindOfWork)customField);

                await _context.SaveChangesAsync(cancellationToken);

                return customField;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ExpertiseArea>> AddNonExistingExpertiseAreas(List<LookupSingleResultModel> expertiseAreas, CancellationToken cancellationToken)
        {
            List<ExpertiseArea> retVal = new List<ExpertiseArea>();
            foreach (var expArea in expertiseAreas ?? new List<LookupSingleResultModel>())
            {
                ExpertiseArea expertiseArea;

                if (expArea.Value >= 0)
                {
                    expertiseArea = _context.ExpertiseAreas.Find(expArea.Value);
                }
                else
                {
                    expertiseArea = (ExpertiseArea)(await AddCustomField(expArea.Name, new ExpertiseArea(), cancellationToken));

                    var adminRole = _context.ApplicationRoles.Where(x => x.Name == UserRoleDefaults.AdminRoleName).FirstOrDefault();

                    if (adminRole is null)
                        throw new ArgumentNullException(nameof(UserRoleDefaults.AdminRoleName));

                    var admins = _context.ApplicationUserRoles.Where(x => x.RoleId == adminRole.Id).Select(x => x.ApplicationUser).ToList();

                    foreach (var admin in admins)
                    {
                        Notification notification = new Notification()
                        {
                            DateTimeCreated = DateTime.UtcNow,
                            DetailsLink = "TODO",
                            IsSeen = false,
                            Message = $"New custom expertise area added: '{expertiseArea.Value}'.",
                            NotificationType = NotificationType.Admin,
                            Receiver = admin
                      };
                        _context.Notifications.Add(notification);
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                }
                retVal.Add(expertiseArea);
            }

            return retVal;
        }


        public async Task<List<KindOfWork>> AddNonExistingKindOfWorks(List<LookupSingleResultModel> kindOfWorks, CancellationToken cancellationToken)
        {
            List<KindOfWork> retVal = new List<KindOfWork>();
            foreach (var kws in kindOfWorks ?? new List<LookupSingleResultModel>())
            {
                KindOfWork kindOfWork;

                if (kws.Value >= 0)
                {
                    kindOfWork = _context.KindOfWorks.Find(kws.Value);
                }
                else
                {
                    kindOfWork = (KindOfWork)(await AddCustomField(kws.Name, new KindOfWork(), cancellationToken));

                    var adminRole = _context.ApplicationRoles.Where(x => x.Name == UserRoleDefaults.AdminRoleName).FirstOrDefault();

                    if (adminRole is null)
                        throw new ArgumentNullException(nameof(UserRoleDefaults.AdminRoleName));

                    var admins = _context.ApplicationUserRoles.Where(x => x.RoleId == adminRole.Id).Select(x => x.ApplicationUser).ToList();


                    foreach (var admin in admins)
                    {
                        Notification notification = new Notification()
                        {
                            DateTimeCreated = DateTime.UtcNow,
                            DetailsLink = "TODO",
                            IsSeen = false,
                            Message = $"New custom kind of work added: '{kindOfWork.Value}'.",
                            NotificationType = NotificationType.Admin,
                            Receiver = admin
                        };
                        _context.Notifications.Add(notification);
                    }

                    await _context.SaveChangesAsync(cancellationToken);
                }
                retVal.Add(kindOfWork);
            }

            return retVal;
        }
    }
}
