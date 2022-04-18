using GestBieresApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace GestBieresApi.Tools
{
    public class TokenService
    {
        private readonly string _issuer, _audience, _secret;

        public TokenService(IConfiguration config)
        {
            _issuer = config.GetSection("tokenValidation").GetSection("issuer").Value;
            _audience = config.GetSection("tokenValidation").GetSection("audience").Value;
            _secret = config.GetSection("tokenValidation").GetSection("secret").Value;
        }

        public string GenerateJWT(AppUser user)
        {
            if (user.Email is null)
            {
                throw new ArgumentNullException();
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Info utilisateur
            Claim[] myClaims = new[]
            {
                new Claim(ClaimTypes.Surname, user.Nickname),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "admin" : "user"),
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken
            (
                claims: myClaims,
                signingCredentials: credentials,
                issuer: _issuer,
                audience: _audience,
                expires: DateTime.Now.AddDays(1)
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }

    }
}
