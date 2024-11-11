using GerencidorDeEventos.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GerencidorDeEventos.Service
{
    public static class TokenService
    {
        //encriptando usuário utilizando JWT
        
        public static string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SenhaToken.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email.ToString() ),
                    new Claim(ClaimTypes.Role, usuario.Roles.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetEmailFromToken(string token) 
        { 
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            
            var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            return emailClaim;

        }

        public static bool IsTokenEmailValid(string token, string expectedEmail)
        {
            var emailFromToken = GetEmailFromToken(token);
            return emailFromToken == expectedEmail;
        }
    }
}
