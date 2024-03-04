using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Options;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using IdentityLearning.Identity.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityLearning.Identity.Tools
{
    internal class TokenGenerator : ITokenGenerator
    {
        private readonly JwtTokenOptions _jwtOptions;

        public TokenGenerator(IOptions<JwtTokenOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public Task<JwtWithInfo> GenerateAccessToken(ApplicationUser user, IEnumerable<Claim> userClaims, IEnumerable<string> roles)
        {
            var guid = Guid.NewGuid();

            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Name!),
                    new Claim(JwtRegisteredClaimNames.Jti, guid.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim("uid", user.Id.ToString())
                }
                .Union(userClaims)
                .Union(roleClaims);

            var jwtToken = GenerateTokenFromOptionsWithClaims(
                expiresInMinutes: _jwtOptions.AccessTokenExpiresInMinutes,
                claims: claims.ToArray());

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var tokenInfo = new TokenInfo()
            {
                Id = guid,
                Expiration = DateTime.Now.AddMinutes(_jwtOptions.AccessTokenExpiresInMinutes),
            };
            return Task.FromResult(new JwtWithInfo(tokenInfo, encodedJwt));
        }

        public Task<JwtWithInfo> GenerateRefreshToken()
        {
            var guid = Guid.NewGuid();

            var jwtToken = GenerateTokenFromOptionsWithClaims(
                expiresInMinutes: _jwtOptions.RefreshTokenExpiresInMinutes,
                claims: new Claim(JwtRegisteredClaimNames.Jti, guid.ToString()));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var tokenInfo = new TokenInfo()
            {
                Id = guid,
                Expiration = DateTime.Now.AddMinutes(_jwtOptions.RefreshTokenExpiresInMinutes),
            };
            return Task.FromResult(new JwtWithInfo(tokenInfo, encodedJwt));
        }

        private JwtSecurityToken GenerateTokenFromOptionsWithClaims(int expiresInMinutes = 60, params Claim[] claims)
        {
            return new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.Now.AddMinutes(expiresInMinutes),
                signingCredentials: new SigningCredentials(
                    key: _jwtOptions.GetSymmetricSecurityKey(),
                    algorithm: SecurityAlgorithms.HmacSha256),
                claims: claims);
        }

        public Guid? GetGuidFromClaims(IEnumerable<Claim> claims)
        {
            var guidClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            if (guidClaim == null)
            {
                return null;
            }

            return Guid.Parse(guidClaim.Value);
        }

        public long? GetUserIdFromClaims(IEnumerable<Claim> claims)
        {
            var userIdClaim = claims.FirstOrDefault(c => c.Type == "uid");
            if (userIdClaim == null)
            {
                return null;
            }

            return long.Parse(userIdClaim.Value);
        }

        public IEnumerable<Claim>? GetClaimsFromToken(string token)
        {
            var decodedJwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return decodedJwtToken.Claims;
        }

        public async Task<bool> IsAccessTokenValidToRefresh(string accessToken)
        {
            var accessTokenValidationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(
                accessToken,
                new TokenValidationParameters
                {
                    ValidateIssuer = _jwtOptions.ValidateIssuer,
                    ValidIssuer = _jwtOptions.Issuer,
                    ValidateAudience = _jwtOptions.ValidateAudience,
                    ValidAudience = _jwtOptions.Audience,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = _jwtOptions.ValidateIssuerSigningKey,
                    IssuerSigningKey = _jwtOptions.GetSymmetricSecurityKey()
                });

            return accessTokenValidationResult.IsValid;
        }

        public async Task<bool> IsRefreshTokenValid(string refreshToken)
        {
            var refreshTokenValidationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(
                refreshToken,
                new TokenValidationParameters
                {
                    ValidateIssuer = _jwtOptions.ValidateIssuer,
                    ValidIssuer = _jwtOptions.Issuer,
                    ValidateAudience = _jwtOptions.ValidateAudience,
                    ValidAudience = _jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = _jwtOptions.ValidateIssuerSigningKey,
                    IssuerSigningKey = _jwtOptions.GetSymmetricSecurityKey()
                });

            return refreshTokenValidationResult.IsValid;
        }
    }
}
