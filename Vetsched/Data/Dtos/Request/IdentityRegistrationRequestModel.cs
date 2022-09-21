using System;
namespace Loader.identity_micro_service.Models
{
    public class IdentityRegistrationRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
