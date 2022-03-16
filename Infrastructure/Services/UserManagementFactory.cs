using GhostWriter.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Application.Common.Models;
using System.IdentityModel.Tokens.Jwt;
using GhostWriter.Domain.Entities;
using GhostWriter.Domain.Defaults;
using System.Threading;
using GhostWriter.Infrastructure.Settings;
using GhostWriter.Application.Defaults;
using GhostWriter.Application.Common.Models.Authentication;

namespace GhostWriter.Infrastructure.Services
{
    public class UserManagementFactory : IUserManagementFactory
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationDbContext _context;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly JWTConfigSetting _jwtConfigSetting;
        private readonly IEmailer _emailer;
        private readonly IHttpContextAccessor _accessor;
        private readonly IPictureService _pictureService;
        private readonly IFileProvider _fileProvider;
        private readonly IWordGenerator _wordGenerator;
        private readonly IProjectTagsService _projectTagsService;

        public UserManagementFactory(UserManager<ApplicationUser> userManager, IProjectTagsService projectTagsService, IApplicationDbContext applicationDbContext, JWTConfigSetting jwtConfigSetting, SigningConfigurations signingConfigurations, IEmailer emailer, IHttpContextAccessor accessor, IPictureService pictureService, IFileProvider fileProvider, IWordGenerator wordGenerator)
        {
            _userManager = userManager;
            _context = applicationDbContext;
            _jwtConfigSetting = jwtConfigSetting;
            _signingConfigurations = signingConfigurations;
            _emailer = emailer;
            _accessor = accessor;
            _pictureService = pictureService;
            _fileProvider = fileProvider;
            _wordGenerator = wordGenerator;
            _projectTagsService = projectTagsService;
        }

        #region Public Functions

        public async Task<ResponseWithPayload<LogInResponse>> Login(LoginModel model)
        {
            var user = await FindUser(model.Username);

            // TODO: think about lockout
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                if (!user.EmailConfirmed)
                    return new ResponseWithPayload<LogInResponse>()
                    {
                        Success = false,
                        Message = "Please confirm your email. If you haven't gotten the email, you can request another one."
                    };

                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = new JwtSecurityToken(
                    issuer: _jwtConfigSetting.ValidIssuer,
                    audience: _jwtConfigSetting.ValidAudience,
                    expires: DateTime.Now.AddHours(_jwtConfigSetting.ValidHours),
                    claims: authClaims,
                    signingCredentials: _signingConfigurations.SigningCredentials
                    );

                return new ResponseWithPayload<LogInResponse>()
                {
                    Success = true,
                    SuccessPayload = new LogInResponse()
                    {
                        User = user.UserName,
                        UserRoles = userRoles.ToList(),
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo
                    }
                };
            }

            return new ResponseWithPayload<LogInResponse>()
            {
                Success = false,
                Message = "Wrong email and/or password."
            };
        }

        public async Task<BasicResponse> RegisterUser(RegisterModel model, CancellationToken cancellationToken)
        {
            var userCanBeCreated = await UserCanBeCreated(model.Username, model.Email);
            if (!userCanBeCreated.Feasible)
                return new ResponseWithPayload<LogInResponse>() { Success = false, Message = userCanBeCreated.Reason };

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                UserName = model.Username,
                NormalizedUserName = model.Username.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                DateCreated = DateTime.UtcNow,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new BasicResponse()
                {
                    Success = false,
                    Message = $"User creation failed! Error: {result.Errors.FirstOrDefault()?.Description}"
                };
                
            var role = FindRoleByName(UserRoleDefaults.CustomerRoleName);

            if (role == null)
                return new BasicResponse()
                {
                    Success = false,
                    Message = "Suitable role for user not found."
                };

            ApplicationUserRole applicationUserRole = new ApplicationUserRole()
            {
                RoleId = role.Id,
                UserId = user.Id,
            };

            _context.ApplicationUserRoles.Add(applicationUserRole);
            await _context.SaveChangesAsync(cancellationToken);

            UserRoleData data = new UserRoleData()
            {
                ApplicationUserRole = applicationUserRole,
                JobsPostedCnt = 0,
                TotalSpent = 0,
                PaymentVerified = false
            };

            _context.UserRoleDatas.Add(data);
            await _context.SaveChangesAsync(cancellationToken);

            SendConfirmationEmail(user);

            return new BasicResponse(){
                Success = true,
                Message = "You have succesfully registered for our services."
            };
        }

        public async Task<BasicResponse> RegisterAuthor(RegisterAuthorInputModel model, CancellationToken cancellationToken)
        {
            var userCanBeCreated = await UserCanBeCreated(model.Username, model.Email);
            if (!userCanBeCreated.Feasible)
                return new BasicResponse() { Success = false, Message = userCanBeCreated.Reason };

            var formatedPicture = _pictureService.ReadPictureFromBase64(model.ProfilePicture);

            if (!formatedPicture.FormatSupported || !formatedPicture.SuccessfullyConverted)
            {
                formatedPicture = new PictureModel()
                {
                    Type = "image/png",
                    FormatSupported = true,
                    SuccessfullyConverted = true,
                    Data = _pictureService.ImageToByteArray(_pictureService.GetDefaultPicture()),
                };
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                NormalizedUserName = model.Username.ToUpper(),
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                BirthDate = model.DateOfBirth,
                DateCreated = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new BasicResponse()
                {
                    Success = false,
                    Message = $"User creation failed! Error: {result.Errors.FirstOrDefault()?.Description}"
                };

            var role = FindRoleByName(UserRoleDefaults.GhostwriterRoleName);

            if(role == null)
                return new BasicResponse()
                {
                    Success = false,
                    Message = "User creation failed! Please check user details and try again."
                };
            
            UserRoleData additionalRoleData = new UserRoleData()
            {
                PaypalPayerID = model.PaypalPayerID,
                PaypalEmail = model.PaypalEmail,
                DirectBooking = model.DirectBooking,
                PagesPerDay = model.PagesPerDay,
                IBAN = model.IBAN,
                ProfileIntroduction = model.ProfileIntroduction,
                PricePerPage = model.PricePerPage,
                AvgPricePerPage = 0,
                HighestDegree = _context.Degrees.Where(x => x.Id == model.HighestDegreeId).FirstOrDefault(),
                Languages = new List<Language>(),
                ExpertiseAreas = new List<ExpertiseArea>(),
                KindOfWorks = new List<KindOfWork>()
            };

            foreach (var languageId in model.LanguageIds ?? new List<int>())
            {
                var language = _context.Languages.Find(languageId);

                if (language != null)
                    additionalRoleData.Languages.Add(language);
            }

            var expertiseAreas = await _projectTagsService.AddNonExistingExpertiseAreas(model.ExpertiseAreas, cancellationToken);

            foreach (var area in expertiseAreas)
                additionalRoleData.ExpertiseAreas.Add(area);

            var kindsOfWork = await _projectTagsService.AddNonExistingKindOfWorks(model.KindOfWorks, cancellationToken);

            foreach (var work in kindsOfWork)
                additionalRoleData.KindOfWorks.Add(work);

            ApplicationUserRole applicationUserRole = new ApplicationUserRole()
            {
                RoleId = role.Id,
                UserId = user.Id,
                UserRoleData = additionalRoleData
            };

            try
            {
                var entity = _context.ApplicationUserRoles.Add(applicationUserRole);
                await _context.SaveChangesAsync(cancellationToken);

                var subfolderName = $"{entity.Entity.UserId}";
                var pictureName = $"{entity.Entity.UserId}_{_fileProvider.GetFileNameExtensionGuid().ToString()}";


                _pictureService.SavePictureInFile(pictureName, new List<string> { FileSystemDefaults.ProfilePicturesLocalPath, subfolderName }, formatedPicture.Type, formatedPicture.Data);
                Picture profilePicture = new Picture()
                {
                    MimeType = formatedPicture.Type,
                    LocalPath = FileSystemDefaults.ProfilePicturesLocalPath + subfolderName,
                    PictureFileName = pictureName,
                    DateCreated = DateTime.UtcNow
                };

                _context.Pictures.Add(profilePicture);
                additionalRoleData.Picture = profilePicture;

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return new BasicResponse()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
           
            try
            {
                SendConfirmationEmail(user);
            }
            catch (Exception ex)
            {
                return new BasicResponse()
                {
                    Success = false,
                    Message = ex.Message
                };
            }


            return new BasicResponse()
            {
                Success = true,
                Message = "You have succesfully registered for our services"
            };
        }

        public async Task<BasicResponse> ConfirmEmail(string username, string token)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return new BasicResponse() { Success = false, Message = "User not found." };


            var result = await _userManager.ConfirmEmailAsync(user, token);

            return new BasicResponse() { Success = result.Succeeded, Message = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email." };
        }

        public async Task<BasicResponse> RequestPasswordChange(string username)
        {
            var user = await FindUser(username);

            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                //TODO Change the method
                var callbackLink = await BuildUri("reset-password", "", token, user);

                _emailer.SendFormatedEmail(new List<string>() { user.Email }, "Reset Password", "resetPassword.html", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("passwordResetLink", callbackLink) });

            }

            return new BasicResponse() { Success = true, Message = "An email has been sent." };
        }


        public async Task<BasicResponse> ResendEmailConfirmation(string username)
        {
            var user = await FindUser(username);

            if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
                SendConfirmationEmail(user);

            return new BasicResponse() { Success = true, Message = "An email has been sent." };
        }

        public async Task<BasicResponse> ResetPassword(PasswordResetModel model)
        {
            var user = await FindUser(model.Username);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                return new BasicResponse() { Success = result.Succeeded, Message = result.Succeeded ? "Password successfully changed." : "Failed to change password." };
            }

            return new BasicResponse() { Success = false, Message = "Failed to change password." };
        }

        public async Task<BasicResponse> ChangePassword(string username, PasswordChangeModel model)
        {
            var user = await FindUser(username);

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                return new BasicResponse() { Success = result.Succeeded, Message = result.Succeeded ? "Password successfully changed." : String.Join(", ", result.Errors.Select(x => x.Description)) };
            }

            return new BasicResponse() { Success = false, Message = "Failed to change password." };
        }

        public async Task<FeasibilityModel> CheckEmailAvailability(string email)
        {
            var userWithEmail = await _userManager.FindByEmailAsync(email);

            if (userWithEmail != null)
                return new FeasibilityModel()
                {
                    Feasible = false,
                    Reason = "An account is already registered with your email address."
                };

            return new FeasibilityModel()
            {
                Feasible = true,
            };
        }

      public async Task<UsernameAvailabilityOutputModel> CheckUsernameAvailability(UsernameAvailabilityInputModel model)
        {
            var userWithUsername = await _userManager.FindByNameAsync(model.Username);

            if (userWithUsername == null)
                return new UsernameAvailabilityOutputModel()
                {
                    Available = true,
                    Message = string.Empty,
                    UsernameSuggestions = new List<string>()
                };

            int suggestionsCnt = 5;
            List<string> usernameSuggestions = new List<string>();

            for (int i = 0; i < suggestionsCnt; i++)
            {
                var username = string.Empty;
                do
                {
                    username = _wordGenerator.GenerateUsernameSuggestion(model);

                } while (await _userManager.FindByNameAsync(username) != null || usernameSuggestions.Contains(username));

                usernameSuggestions.Add(username);
            }

            
            return new UsernameAvailabilityOutputModel()
            {
                Available = false,
                Message = $"Username {model.Username} is already is use.",
                UsernameSuggestions = usernameSuggestions
            };
        }

        public ApplicationUser FindUserById(int id)
        {
            var user = _context.ApplicationUsers.Find(id);

            return user;
        }

        public async Task<ApplicationUser> FindUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                user = await _userManager.FindByEmailAsync(username);

            return user;
        }

        public ApplicationRole FindRoleByName(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException(nameof(roleName));

            var query = from ar in _context.ApplicationRoles
                        where ar.Name == roleName
                        select ar;

            return query.FirstOrDefault();
        }

        public IQueryable<UserRoleData> GetUsersAdditionalData(string username, string rolename)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            var customerRoleId = _context.ApplicationRoles.Where(x => x.Name == rolename).FirstOrDefault();

            if (customerRoleId == null)
                throw new ArgumentNullException(nameof(rolename));

            var retVal = _context.UserRoleDatas.Where(x => x.ApplicationUserRole.RoleId == customerRoleId.Id && x.ApplicationUserRole.ApplicationUser.UserName == username);

            return retVal;
        }

        public List<ApplicationUser> GetAllAdminUsers()
        {
            var adminRole = _context.ApplicationRoles.Where(x => x.Name == UserRoleDefaults.AdminRoleName).FirstOrDefault();

            if (adminRole is null)
                throw new ArgumentNullException(nameof(UserRoleDefaults.AdminRoleName));

            return _context.ApplicationUserRoles.Where(x => x.RoleId == adminRole.Id).Select(x => x.ApplicationUser).ToList();
        }

        public int FindRoleIdByName(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException(nameof(roleName));

            var query = from ar in _context.ApplicationRoles
                        where ar.Name == roleName
                        select ar.Id;

            return query.FirstOrDefault();
        }

        public bool IsInRole(ApplicationUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException(nameof(roleName));

            var query = from ur in _context.ApplicationUserRoles
                        join ar in _context.ApplicationRoles on ur.RoleId equals ar.Id
                        where ur.UserId == user.Id && ar.Name == roleName
                        select ur;

            return query.Any();
        }

        public async Task<IList<ApplicationUserRole>> GetUserRoles(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            var query = from au in _context.ApplicationUserRoles
                        where au.UserId == applicationUser.Id
                        select au;

            return query.ToList();
        }

        #endregion

        #region Private Functions

        private async Task<string> BuildUri(string actionName, string controllerName, string token, ApplicationUser user)
        {

            string scheme = _accessor.HttpContext.Request.Scheme;
            string host = _accessor.HttpContext.Request.Host.Host;
            int port = _accessor.HttpContext.Request.Host.Port ?? 443;
            var path = controllerName != null ? $"{controllerName}/{actionName}" : $"{actionName}";
            var extraValue = $"?username={WebUtility.UrlEncode(user.UserName)}&token={WebUtility.UrlEncode(token)}";

            UriBuilder uriBuilder = new UriBuilder(scheme, host, port, path, extraValue);
            
            return uriBuilder.Uri.AbsoluteUri;
        }

        private async void SendConfirmationEmail(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        
            var callbackLink = await BuildUri("email-confirmed", "", token, user);
            //var callbackLink = await BuildUri("ConfirmEmail", "Authenticate", token, user);

            _emailer.SendFormatedEmail(new List<string>() { user.Email }, "Welcome to Studi-Autoren", "confirmEmail.html", new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("emailConfirmationLink", callbackLink) });
        }

        public async Task<FeasibilityModel> UserCanBeCreated(string username, string email)
        {
            var userExists = await _userManager.FindByNameAsync(username);
            var userWithEmail = await _userManager.FindByEmailAsync(email);

            if (userExists != null)
                return new FeasibilityModel()
                {
                    Feasible = false,
                    Reason = "Username already in use."
                };

            if (userWithEmail != null)
                return new FeasibilityModel()
                {
                    Feasible = false,
                    Reason = "An account is already registered with your email address."
                };

            return new FeasibilityModel()
            {
                Feasible = true,
            };
        }


        #endregion
    }
}
