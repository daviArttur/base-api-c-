using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Testes.src.app.interfaces;

namespace Testes.src.app.services
{
    public class JwtService(JwtSecurityTokenHandler _handler, IConfiguration configuration) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly JwtSecurityTokenHandler _handler = _handler;
        public string GenerateToken(int userId)
        {
            byte[] jwtSecretKey = this.GetJWTSecretKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new("sub", userId.ToString()),
                    new("userId", userId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtSecretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenPayload = this._handler.CreateToken(tokenDescriptor);
            return this._handler.WriteToken(tokenPayload);
        }

        public void DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            Console.WriteLine(tokenHandler.ReadJwtToken(token));
        }

        public int RecoveryUserIdFromHttpContext(HttpContext httpContext)
        {
            try
            {
                return Convert.ToInt32(httpContext.User.FindFirstValue("userID"));
            }
            catch
            {
                throw new IdMissingFromAccessTokenException();
            }
        }

        private byte[] GetJWTSecretKey()
        {
            var jwtSecretKey = this._configuration["JwtSettings:SecretKey"];
            if (jwtSecretKey == null) throw new JwtSecretKeyNotFound();
            return Encoding.UTF8.GetBytes(jwtSecretKey);
        }
    }
}