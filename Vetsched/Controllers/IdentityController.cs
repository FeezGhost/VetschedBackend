using Loader.identity_micro_service.Core.Dto;
using Loader.identity_micro_service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vetsched.Data.Dtos;
using Vetsched.Data.Entities;
using Vetsched.Services;

namespace Vetsched.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController: ControllerBase
    {

        public IdentityServiceInterface _identityService;
        private readonly UserManager<ApplicationUser> _applicationUser;
        public IdentityController(
            IdentityServiceInterface identity,
            UserManager<ApplicationUser> applicationUser
            )
        {
            _identityService = identity;
            _applicationUser = applicationUser;
        }

        [HttpGet]
        public IActionResult LoggedInUser()
        {
            return Ok();
        }


        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(IdentityAuthRequestModel authRequest)
        {
            var loginResponse = await _identityService.Login(authRequest);
            if (loginResponse.Item1 == 200)
            {
                var loggedInUser = await _applicationUser.FindByEmailAsync(authRequest.Username);
                return Ok(new GenericDto<string> { StatusCode = 200, StatusMessage = "OTP Sent!", Data = loginResponse.Item2 });
            }
            else
            {
                return Ok(new GenericDto<string> { StatusCode = 403, StatusMessage = loginResponse.Item2 });
            }
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult<GenericDto<string>>> SignUp(SignUpRequestDto registrationRequest)
        {
            var data = await _identityService.Register(registrationRequest);
            if (data.StatusCode == 200)
            {
                //var confirmationLink = "";

                //confirmationLink = _configuration["URLs:ClientURL"] + "emailConfirmation?EmailToken=" + System.Net.WebUtility.UrlEncode(data.EmailToken) + "&Email=" + registrationRequest.Email + "&User=AccountOwner";

                //if (registrationRequest.AccountId is not null)
                //{
                //    confirmationLink = _configuration["URLs:ClientURL"] + "emailConfirmation?EmailToken=" + System.Net.WebUtility.UrlEncode(data.EmailToken) + "&Email=" + registrationRequest.Email + "&User=JoinAccount&Account=" + registrationRequest.AccountId;
                //}

                //UserDetailsForEmail User = new UserDetailsForEmail();
                //Emaildto emailObj = new Emaildto();
                //List<UserDetailsForEmail> usersList = new List<UserDetailsForEmail>();

                //emailObj.type = EmailTypes.emailVerification;
                //User.FirstName = registrationRequest.FirstName;
                //User.LastName = registrationRequest.LastName;
                //User.Email = registrationRequest.Email;
                //User.Phone = registrationRequest.Phone;
                //emailObj.url = confirmationLink;

                //usersList.Add(User);
                //emailObj.User = usersList;
                //try
                //{
                //    await _externalCommunicationService.SendEmail(emailObj);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex);
                //}
            }
            return Ok(data);
        }

        [Authorize]
        [HttpPost("AuthPing")]
        public async Task<IActionResult> AuthPing()
        {
            return Ok();
        }
    }
}
