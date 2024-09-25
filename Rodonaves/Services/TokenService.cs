using Microsoft.IdentityModel.Tokens;
using Rodonaves.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Rodonaves.Services
{
    public class TokenService
    {
        public string GenerateToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigurationJWT.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
