using HotelHub.Models;
using Intuit.Ipp.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelHub.Services {
    public class TokenService {
        public static object GenerateToken(Hospede hospede) {

            var key = Encoding.ASCII.GetBytes(Key.Secret);
            var tokenConfig = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim("hospedeId", hospede.HospedeId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new {
                Token = tokenString,
            };
        }
    }
}
