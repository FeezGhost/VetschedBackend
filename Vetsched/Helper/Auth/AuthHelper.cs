using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vetsched.Data.Entities;

namespace Loader.infrastructure.Helper.Auth
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IConfiguration _configuration;
        public AuthHelper(
            IConfiguration configuration
            )
        {
            _configuration = configuration;
        }
        //public string GenerateJSONWebToken(ApplicationUser userInfo)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM");
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);


        //    return tokenHandler.WriteToken(token);
        //}
        public async Task<string> GenerateJSONWebToken(ApplicationUser user)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            authClaims.Add(new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String));
            authClaims.Add(new Claim(new IdentityOptions().ClaimsIdentity.UserIdClaimType, Convert.ToString(user.Id), ClaimValueTypes.String));
            //if (user.ActiveAccount != null)
            //{
            //    authClaims.Add(new Claim("AccountId", user.ActiveAccount.ToString(), ClaimValueTypes.Integer32));
            //}
            //else
            //{
            //    authClaims.Add(new Claim("AccountId", "0", ClaimValueTypes.Integer32));
            //}
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var tokenExpiryInMin = Convert.ToInt32(_configuration["JWT:ValidityInMin"]);
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenExpiryInMin),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(int UserID)
        {
            var authClaims = new List<Claim>
                {
                    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserID", UserID.ToString(), ClaimValueTypes.String)
                };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTRefresh:Secret"]));
            var tokenExpiryInDays = Convert.ToInt32(_configuration["JWTRefresh:ValidityInDays"]);
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(tokenExpiryInDays),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
