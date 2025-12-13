using BackendApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BackendApi.Utils
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _Config;
        public JwtTokenGenerator(IConfiguration Config)
        {
            _Config = Config;       
        }

        public string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role),

            };

            SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["Jwt:Key"]));
            SigningCredentials creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _Config["Jwt:Issuer"],
                audience: _Config["Jwt:Audience"],
                claims: claims,
                expires :DateTime.Now.AddMinutes(
                    Convert.ToDouble(_Config["Jwt:ExpiryMinutes"])
                    ),
                signingCredentials:creds
                
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
