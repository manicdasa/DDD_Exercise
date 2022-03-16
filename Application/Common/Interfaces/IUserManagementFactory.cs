using GhostWriter.Application.Common.Models;
using GhostWriter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace GhostWriter.Application.Common.Interfaces
{
    public interface IUserManagementFactory
    {
        Task<ResponseWithPayload<LogInResponse>> Login(LoginModel model);
        Task<BasicResponse> RegisterUser(RegisterModel model, CancellationToken cancellationToken);
        Task<BasicResponse> RegisterAuthor(RegisterAuthorInputModel model, CancellationToken cancellationToken);
        Task<BasicResponse> ConfirmEmail(string username, string token);
        Task<BasicResponse> RequestPasswordChange(string username);
        Task<BasicResponse> ResendEmailConfirmation(string username);
        Task<BasicResponse> ResetPassword(PasswordResetModel model);
        Task<BasicResponse> ChangePassword(string username, PasswordChangeModel model);
        Task<FeasibilityModel> CheckEmailAvailability(string email);
        Task<UsernameAvailabilityOutputModel> CheckUsernameAvailability(UsernameAvailabilityInputModel model);
        Task<FeasibilityModel> UserCanBeCreated(string username, string email);

        //Perhaps store the methods below in a separate factory
        Task<ApplicationUser> FindUser(string username);
        IQueryable<UserRoleData> GetUsersAdditionalData(string username, string rolename);
        List<ApplicationUser> GetAllAdminUsers();
        ApplicationUser FindUserById(int id);
        bool IsInRole(ApplicationUser user, string roleName);
        ApplicationRole FindRoleByName(string roleName);
        int FindRoleIdByName(string roleName);
    }
}
