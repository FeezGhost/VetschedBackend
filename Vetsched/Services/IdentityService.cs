using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loader.identity_micro_service.Core.Dto;
using Loader.identity_micro_service.Models;
using Loader.infrastructure.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vetsched.Data.DBContexts;
using Vetsched.Data.Dtos;
using Vetsched.Data.Entities;

namespace Vetsched.Services
{
    public class IdentityService : IdentityServiceInterface
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<ApplicationRole> _roleManager;
        private VetschedContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAuthHelper _authHelper;

        public IdentityService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            VetschedContext context,
            //RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration, IAuthHelper authHelper
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _authHelper = authHelper;
            //_roleManager = roleManager;
        }

        public enum DistanceUnit { StatuteMile, Kilometer, NauticalMile };

        private double Distance(double lat1, double lon1, double lat2, double lon2, DistanceUnit unit)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case DistanceUnit.Kilometer:
                    return dist * 1.609344;
                case DistanceUnit.NauticalMile:
                    return dist * 0.8684;
                default:
                case DistanceUnit.StatuteMile: //Miles
                    return dist;
            }
        }
        public async Task<(int, string)> Login(IdentityAuthRequestModel authRequest)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(authRequest.Username, authRequest.Password, isPersistent: false, lockoutOnFailure: false);
            if (loginResult.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(authRequest.Username);
                if (user!= null && user.EmailConfirmed == true)
                {
                    return (200, await _authHelper.GenerateJSONWebToken(user));
                }
                else
                {
                    return (401, "Your email is not verified, please check your inbox");
                }
            }
            else
            {
                return (401, "The login credentials don't match, please try again!");
            }
        }
        //public async Task<string> OTPVerification(OTPRequestDto oTPRequestDto)
        //{
        //    var user = await _userManager.FindByEmailAsync(oTPRequestDto.UserName);

        //    if (user.VerificationCode == oTPRequestDto.OTP)
        //    {
        //        return await _authHelper.GenerateJSONWebToken(user);
        //    }
        //    else
        //        return "Please enter valid OTP";

        //}
        public async Task<GenericDto<string>> ForgetPassword(string email)
        {
            string response = String.Empty;
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new GenericDto<string> { StatusCode = 403, StatusMessage = "Email Not Found" };

            var Emailtoken = await _userManager.GeneratePasswordResetTokenAsync(user);
            response = await _authHelper.GenerateJSONWebToken(user);
            return new GenericDto<string> { StatusCode = 200, StatusMessage = "Email Sent Successful!", Token = response, Data = Emailtoken };
        }
        public async Task<GenericDto<string>> Register(SignUpRequestDto registrationRequest)
        {
            string response = String.Empty;

            var defaultUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = registrationRequest.FirstName,
                LastName = registrationRequest.LastName,
                PhoneNumber = registrationRequest.Phone,
                UserName = registrationRequest.Email,
                JobTitle = registrationRequest.JobTitle,
                Email = registrationRequest.Email,
                PasswordHash = registrationRequest.Password,
                Gender = registrationRequest.Gender,
                VerificationCode = "0332A",
                ProfileImage = "",
                TimeZoneInfo = "",
                CreatedBy = "",
                ImageUri = "",
                ModifiedBy = ""

            };
            var registrationResult = await _userManager.CreateAsync(defaultUser, registrationRequest.Password);
            if (registrationResult.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(defaultUser.Email);
                var Emailtoken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var result = await _userManager.ConfirmEmailAsync(user, Emailtoken);
                response = await _authHelper.GenerateJSONWebToken(user);
                return new GenericDto<string> { StatusCode = 200, StatusMessage = "Signup Successful!", Token = response, Data = Emailtoken };
            }
            else
            {
                foreach (var err in registrationResult.Errors)
                {
                    response += err.Description;
                }
                return new GenericDto<string> { StatusCode = 403, StatusMessage = response };
            }
        }

        //public async Task<GenericDto<bool>> CreateUser(SignUpRequestDto newUserDto, Guid accountId, string state, Guid LoggedInUserId, string token = null)
        //{
        //    bool isFreemium = false;
        //    var account = _repositoryAccount.GetOneDefaultWithInclude(x => x.Id == accountId, "Licenses", "AccountUsers", "ParentAccount", "ParentAccount.Licenses");
        //    if (account is null)
        //    {
        //        return new RequestResponse() { StatusCode = 404, StatusMessage = "Account Not Found" };
        //    }

        //    if (account.IsActive == false)
        //    {
        //        return new RequestResponse() { StatusCode = 401, StatusMessage = "Account Is Inactive" };
        //    }

        //    if (account.ParentAccount is not null)
        //    {
        //        if (account.ParentAccount.Licenses.Count() == 0)
        //        {
        //            isFreemium = true;
        //            if (account.AccountUsers.Count() >= 3)
        //            {
        //                return new RequestResponse() { StatusCode = 404, StatusMessage = "Max Licenese Limit Reached! Can not make more account." };
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (account.Licenses.Count() == 0)
        //        {
        //            isFreemium = true;
        //            if (account.AccountUsers.Count() >= 3)
        //            {
        //                return new RequestResponse() { StatusCode = 404, StatusMessage = "Max Licenese Limit Reached! Can not make more account." };
        //            }
        //        }
        //    }
        //    var password = "Gaga2022!" + Guid.NewGuid();

        //    var defaultUser = new ApplicationUser
        //    {
        //        Id = Guid.NewGuid(),
        //        FirstName = newUserDto.FirstName,
        //        LastName = newUserDto.LastName,
        //        PhoneNumber = newUserDto.Phone,
        //        UserName = newUserDto.Email,
        //        Email = newUserDto.Email,
        //        PasswordHash = password,
        //        Gender = newUserDto.Gender,
        //        JobTitle = newUserDto.JobTitle,
        //        ActiveAccount=accountId,
        //    };

        //    //Get Timezone From country and geo-location using Noda Time (beacuse it's diverse) and convert it to TimeZoneInfo
        //    if (newUserDto.Country is not null)
        //    {
        //        var zones = TzdbDateTimeZoneSource.Default.ZoneLocations.Where(x => x.CountryName == newUserDto.Country).AsQueryable();
        //        if (!double.IsNaN(newUserDto.Lng))
        //        {
        //            zones = zones.OrderBy(o => this.Distance(o.Latitude, newUserDto.Lng, o.Latitude, o.Longitude, DistanceUnit.Kilometer));
        //        }
        //        var bestZone = zones.FirstOrDefault();
        //        var dateTimeZone = TzdbDateTimeZoneSource.Default.ForId(bestZone.ZoneId);
        //        string windowsTimeZoneName = TZConvert.IanaToWindows(dateTimeZone.ToString());
        //        var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneName);
        //        defaultUser.TimeZoneInfo = timeZoneInfo.Id.ToString();
        //    }
        //    AccountUser loggedInAccountUser = await _repositoryAccountUser.GetFirst(x => x.AccountId == accountId && x.UserId == LoggedInUserId);
        //    var registrationResult = await _userManager.CreateAsync(defaultUser, password);
        //    if (registrationResult.Succeeded)
        //    {
        //        AccountUser accountUserdto = new AccountUser()
        //        {
        //            UserId = defaultUser.Id,
        //            AccountUserStatus = AccountUserStatus.Inactive,
        //            AccountId = accountId,
        //            JobTitle = defaultUser.JobTitle,
        //            PhoneNumber = defaultUser.PhoneNumber,
        //            AccountUserState = state.Equals("invited") ? AccountUserState.Invited : AccountUserState.NotJoined,
        //            UserApprovalLevel = UserApprovalLevel.Account,
        //            UserRequestPoint = UserRequestPoint.SubAccount,
        //            UserRequestPointId = accountId,
        //        };
        //        //add account user to add at parent level
        //        var accountDetails = await _repositoryAccount.GetFirst(x => x.Id == accountId);

        //        if (loggedInAccountUser.RoleId == ApplicationRoleConstants.AccountOwner)
        //        {
        //            accountUserdto.UserApprovalLevel = UserApprovalLevel.Approved;
        //        }
        //        if (accountDetails.ParentAccountId == null)
        //        {
        //            accountUserdto.UserRequestPoint = UserRequestPoint.Account;
        //        }
        //        else
        //        {
        //            accountUserdto.UserRequestPoint = UserRequestPoint.SubAccount;
        //        }
        //        AccountUser accountUser = await _repositoryAccountUser.AddAsync(accountUserdto);
        //        //TODO: Send Mediatr call to AddAccountOwner if freemium true
        //        if (isFreemium == true)
        //        {

        //            List<Guid> userIds = new List<Guid>();
        //            userIds.Add(accountUser.UserId);
        //            await _externalCommunicationService.CreateAccountOwnerExternalComunication(userIds, token);
                    
        //        }
        //        if (newUserDto.LicenseTypeIds.Count()>0)
        //        await AllocateLicenses(newUserDto.LicenseTypeIds, accountUser.Id);
        //        var user = await _userManager.FindByEmailAsync(defaultUser.Email);
        //        var Emailtoken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //        if(accountDetails.ParentAccountId != null)
        //        {
        //            AccountUser parentAccountUser = new AccountUser()
        //            {
        //                UserId = defaultUser.Id,
        //                AccountUserStatus = AccountUserStatus.Pending,
        //                AccountId =(Guid)accountDetails.ParentAccountId,
        //                JobTitle = defaultUser.JobTitle,
        //                PhoneNumber = defaultUser.PhoneNumber,
        //                AccountUserState = state.Equals("invited") ? AccountUserState.Invited : AccountUserState.NotJoined,
        //                UserApprovalLevel = accountUserdto.UserApprovalLevel,
        //                UserRequestPoint = accountUserdto.UserRequestPoint,
        //                UserRequestPointId = accountId,

        //            };

        //            if (loggedInAccountUser.RoleId == ApplicationRoleConstants.AccountOwner)
        //            {
        //                parentAccountUser.AccountUserStatus = AccountUserStatus.Inactive;
        //            }
        //            var licence = await _repositoryLicense.GetFirst(x => x.AccountId == parentAccountUser.AccountId);
        //            if (licence == null)
        //            {
        //                var data = await _repositoryAccountUser.GetMany(x => x.AccountId == parentAccountUser.AccountId && x.Deleted == false);
        //                if (data.Count > 3)
        //                {
        //                    return new RequestResponse() { StatusCode = 404, StatusMessage = "Max Licenese Limit Reached! Can not make more account." };

        //                }
        //            }
        //            AccountUser accountUserP = await _repositoryAccountUser.AddAsync(parentAccountUser);
        //            var owners = _repositoryAccountUser.GetManyIQueryable(x => x.AccountId == accountUserP.AccountId && x.RoleId == ApplicationRoleConstants.AccountOwner).Select(x => x.UserId).ToList();
        //            var totalRequests = (await _repositoryAccountUser.GetMany(x => x.AccountUserStatus == infrastructure.Enums.AccountUserStatus.Pending && x.AccountId == accountUserP.AccountId)).Count();
        //            var userLevel = accountDetails.ParentAccountId != null ? "SubAcc" : "Acc";
        //            ApplicationUser LoggedInUser = await _userManager.FindByIdAsync(LoggedInUserId.ToString());
        //            List<ApplicationUser> ownerUsers =  _userManager.Users.Where(x => owners.Contains(x.Id)).ToList();
        //            foreach (var ownerUser in ownerUsers)
        //            {
        //                await SendEmailNotification(ownerUser, accountDetails, LoggedInUser, userLevel,totalRequests);
        //            }
        //        }
        //        return new RequestResponse { StatusCode = 200, StatusMessage = "User Created Sucessfully!", EmailToken = Emailtoken };
        //    }
        //    else
        //    {
        //        string response = "";
        //        foreach (var err in registrationResult.Errors)
        //        {
        //            response += err.Description;
        //        }
        //        return new RequestResponse { StatusCode = 403, StatusMessage = response };
        //    }
        //}
        
        public async Task<GenericDto<string>> ConfirmUserEmail(string Token, string Email)
        {
            string response = String.Empty;
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return new GenericDto<string> { StatusCode = 403, StatusMessage = "Something went wrong! User not found" };
            }
            if (user.EmailConfirmed == true)
            {
                return new GenericDto<string> { StatusCode = 409, StatusMessage = "Something went wrong! Email Already Verified!" };
            }
            //Token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var result = await _userManager.ConfirmEmailAsync(user, Token);
            if (result.Succeeded)
            {
                response = await _authHelper.GenerateJSONWebToken(user);
                return new GenericDto<string> { StatusCode = 200, StatusMessage = "Email Confirm Successfully!", Data =  response, Token = response };
            }
            else
            {
                return new GenericDto<string> { StatusCode = 403, StatusMessage = "Something went wrong! Possible reason email token expired" };
            }
        }
        public async Task<GenericDto<bool>> ResetPassword(Guid Id, string Password)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, Password);
            if (result.Succeeded)
            {
                return new GenericDto<bool> { StatusCode = 200, StatusMessage = "Password reset successfully!", Data = true };
            }
            else
            {
                return new GenericDto<bool> { StatusCode = 403, StatusMessage = "Error", Data = false };
            }
        }
        public async Task<bool> Logout(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await _signInManager.SignOutAsync();

                return true;
            }
            return false;
        }

    }
}
