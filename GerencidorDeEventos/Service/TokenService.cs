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
                    new Claim("id", usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Administrador.ToString())
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

        public static string GetCpfFromToken(string token) 
        { 
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            
            var cpfClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "cpf")?.Value;

            return cpfClaim;

        }

        public static string GetEmailFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            return emailClaim;

        }

        public static string GetIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            return idClaim;

        }

        public static string GetRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(token);

            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            return roleClaim;
        }

        public static bool IsTokenCpfValid(string token, string expectedCpf)
        {
            var cpfFromToken = GetCpfFromToken(token);
            return cpfFromToken == expectedCpf;
        }


    }
}
