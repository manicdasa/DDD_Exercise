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
using GhostWriter.Application.Common.Models.Shared;

namespace GhostWriter.Application.User.Commands.UpdateUser
{
    public class EditAuthorPrivateInfoCommand 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool DirectBooking { get; set; }
        public string ProfileIntroduction { get; set; }
        public string Description { get; set; }
        public int HighestDegree { get; set; }
        public List<LookupSingleResultModel> ExpertiseAreas { get; set; }
        public List<LookupSingleResultModel> KindOfWorks { get; set; }
        public List<int> LanguageIds { get; set; }
    }

    public class EditAuthorPrivateInfoCommandExtended : EditAuthorPrivateInfoCommand, IRequest<OutputModel>, IMapFrom<EditAuthorPrivateInfoCommand>
    {
        public string GHWUsername { get; set; }
    }

    public class EditAuthorPrivateInfoCommandHandler : IRequestHandler<EditAuthorPrivateInfoCommandExtended, OutputModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IProjectTagsService _projectTagsService;

        public EditAuthorPrivateInfoCommandHandler(IApplicationDbContext context, IProjectTagsService projectTagsService, IUserManagementFactory userManagementFactory)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _projectTagsService = projectTagsService;
        }

        public async Task<OutputModel> Handle(EditAuthorPrivateInfoCommandExtended request, CancellationToken cancellationToken)
        {
            var ghw = await _userManagementFactory.FindUser(request.GHWUsername);

            if (ghw == null)
                throw new NotFoundException($"Author {request.GHWUsername} not found.");

            if (!_userManagementFactory.IsInRole(ghw, UserRoleDefaults.GhostwriterRoleName))
                throw new AuthorizationException($"User {request.GHWUsername} is unauthorized edit private author information.");
          
            var ghwRoleId = _userManagementFactory.FindRoleIdByName(UserRoleDefaults.GhostwriterRoleName);
            var ghwInfo = _context.UserRoleDatas.Where(x => x.ApplicationUserRole.UserId == ghw.Id && x.ApplicationUserRole.RoleId == ghwRoleId).FirstOrDefault();

            if (ghwInfo == null)
                throw new NotFoundException($"Private information about the author {request.GHWUsername} not found.");

            try
            {
                ghw.FirstName = request.FirstName;
                ghw.LastName = request.LastName;
                ghw.BirthDate = request.BirthDate;
                ghwInfo.DirectBooking = request.DirectBooking;
                ghwInfo.ProfileIntroduction = request.ProfileIntroduction;
                ghwInfo.HighestDegree = request.HighestDegree != default(int) ? _context.Degrees.Find(request.HighestDegree) : null;
                UpdateLanguages(request.LanguageIds, ref ghwInfo);
                ghwInfo.ExpertiseAreas = await UpdateExpertiseAreas(request.ExpertiseAreas, ghwInfo, cancellationToken);
                ghwInfo.KindOfWorks = await UpdateKindOfWorks(request.KindOfWorks, ghwInfo, cancellationToken);

                _context.UserRoleDatas.Update(ghwInfo);
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
                    Message = ex.Message
                };
            }
        }

        private void UpdateLanguages(List<int> languageIds, ref UserRoleData ghwInfo)
        {
            for(int i = ghwInfo.Languages.Count - 1; i >= 0; i--)
            {
                ghwInfo.Languages.Remove(ghwInfo.Languages.ElementAt(i));
            }

            var languages = new List<Language>();
            foreach (var languageId in languageIds ?? new List<int>())
            {
                var language = _context.Languages.Find(languageId);

                if (language != null)
                    languages.Add(language);
            }
            ghwInfo.Languages = languages;
        }

        private async Task<List<ExpertiseArea>> UpdateExpertiseAreas(List<LookupSingleResultModel> expertiseAreas, UserRoleData ghwInfo, CancellationToken cancellationToken)
        {
            for (int i = ghwInfo.ExpertiseAreas.Count - 1; i >= 0; i--)
            {
                ghwInfo.ExpertiseAreas.Remove(ghwInfo.ExpertiseAreas.ElementAt(i));
            }
            _context.UserRoleDatas.Update(ghwInfo);
            await _context.SaveChangesAsync(cancellationToken);

             var newExpertiseAreas = await _projectTagsService.AddNonExistingExpertiseAreas(expertiseAreas, cancellationToken);

            return newExpertiseAreas;
        }

        private async Task<List<KindOfWork>> UpdateKindOfWorks(List<LookupSingleResultModel> kindOfWorks, UserRoleData ghwInfo, CancellationToken cancellationToken)
        {
            for (int i = ghwInfo.KindOfWorks.Count - 1; i >= 0; i--)
            {
                ghwInfo.KindOfWorks.Remove(ghwInfo.KindOfWorks.ElementAt(i));
            }
            _context.UserRoleDatas.Update(ghwInfo);
            await _context.SaveChangesAsync(cancellationToken);

            var newkindOfWorks = await _projectTagsService.AddNonExistingKindOfWorks(kindOfWorks, cancellationToken);

            return newkindOfWorks;
        }
    }

}
