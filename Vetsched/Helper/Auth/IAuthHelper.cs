using Vetsched.Data.Entities;

namespace Loader.infrastructure.Helper.Auth
{
    public interface IAuthHelper
    {
        Task<string> GenerateJSONWebToken(ApplicationUser user);
        string GenerateRefreshToken(int UserID);
    }
}
