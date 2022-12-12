using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpendingManagement.Application.Accounts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SpendingManagement.Application.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> CreateTokenAsync(LoginRequest request)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, request.UserName));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}