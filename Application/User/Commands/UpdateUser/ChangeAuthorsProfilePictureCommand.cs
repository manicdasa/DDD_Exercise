using AutoMapper;
using GhostWriter.Application.Common.Exceptions;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using GhostWriter.Application.Defaults;
using GhostWriter.Application.DTOs;
using GhostWriter.Domain.Defaults;
using GhostWriter.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GhostWriter.Application.User.Commands.UpdateUser
{
    public class ChangeAuthorsProfilePictureCommand : IRequest<OutputModel>
    {
        public string Picture { get; set; }
    }

    public class ChangeAuthorsProfilePictureCommandExtended : ChangeAuthorsProfilePictureCommand, IRequest<OutputModel>
    {
        public string GHWUsername { get; set; }
    }

    public class ChangeAuthorsProfilePictureCommandHandler : IRequestHandler<ChangeAuthorsProfilePictureCommandExtended, OutputModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagementFactory _userManagementFactory;
        private readonly IPictureService _pictureService;
        private readonly IFileProvider _fileProvider;

        public ChangeAuthorsProfilePictureCommandHandler(IApplicationDbContext context, IUserManagementFactory userManagementFactory, IPictureService pictureService, IFileProvider fileProvider)
        {
            _context = context;
            _userManagementFactory = userManagementFactory;
            _pictureService = pictureService;
            _fileProvider = fileProvider;
        }

        public async Task<OutputModel> Handle(ChangeAuthorsProfilePictureCommandExtended request, CancellationToken cancellationToken)
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
                var formatedPicture = _pictureService.ReadPictureFromBase64(request.Picture);
                if (!formatedPicture.FormatSupported || !formatedPicture.SuccessfullyConverted)
                    return new OutputModel()
                    {
                        Success = false,
                        Message = $"Picture format not supported"
                    };

                var subfolderName = $"{ghw.Id}";
                var pictureName = $"{ghw.Id}_{_fileProvider.GetFileNameExtensionGuid().ToString()}";

                _pictureService.SavePictureInFile(pictureName, new List<string> { FileSystemDefaults.ProfilePicturesLocalPath, subfolderName }, formatedPicture.Type, formatedPicture.Data);
                Picture profilePicture = new Picture()
                {
                    MimeType = formatedPicture.Type,
                    LocalPath = FileSystemDefaults.ProfilePicturesLocalPath + subfolderName,
                    PictureFileName = pictureName,
                    DateCreated = DateTime.UtcNow
                };

                _context.Pictures.Add(profilePicture);
                ghwInfo.Picture = profilePicture;

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
    }
}
