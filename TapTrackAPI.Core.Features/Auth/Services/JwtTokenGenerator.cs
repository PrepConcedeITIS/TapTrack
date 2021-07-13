using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TapTrackAPI.Core.Features.Auth.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _config;
        private readonly IHostEnvironment _environment;

        public JwtTokenGenerator(IConfiguration config, IHostEnvironment environment)
        {
            _config = config;
            _environment = environment;
        }

        public string GenerateToken(User user)
        {
            var (key, issuer, audience) = GetJwtParams();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(3),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private (string, string, string) GetJwtParams()
        {
            var jwtSecretKeyEnvironment = Environment.GetEnvironmentVariable("JWT_Key");
            var jwtIssuerEnvironment = Environment.GetEnvironmentVariable("JWT_Issuer");
            var jwtAudienceEnvironment = Environment.GetEnvironmentVariable("JWT_Audience");
            if (!_environment.IsDevelopment() &&
                jwtSecretKeyEnvironment != null && jwtIssuerEnvironment != null && jwtAudienceEnvironment != null)
            {
                return (jwtSecretKeyEnvironment, jwtIssuerEnvironment, jwtAudienceEnvironment);
            }

            return (_config["Jwt:SecretKey"], _config["Jwt:Issuer"], _config["Jwt:Audience"]);
        }
    }
}