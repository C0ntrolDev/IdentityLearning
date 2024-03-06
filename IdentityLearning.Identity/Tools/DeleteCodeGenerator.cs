using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
    internal class DeleteCodeGenerator : IDeleteCodeGenerator
    {
        private readonly JwtTokenOptions _jwtOptions;

        public DeleteCodeGenerator(IOptions<JwtTokenOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateDeleteCode(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id.ToString())
            };

            var code = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.Now.AddMinutes(_jwtOptions.DeleteCodeExpiresInMinutes),
                signingCredentials: new SigningCredentials(
                    key: _jwtOptions.GetSymmetricSecurityKey(),
                    algorithm: SecurityAlgorithms.HmacSha256),
                claims: claims);

            var jwtCode = new JwtSecurityTokenHandler().WriteToken(code);
            var encodedCode = Base64UrlEncoder.Encode(jwtCode);
            return encodedCode;
        }

        public async Task<Result<object>> ValidateDeleteCode(ApplicationUser user, string code)
        {
            var decodedCode = Base64UrlEncoder.Decode(code);

            var readTokenResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(
                decodedCode,
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

            if (readTokenResult.IsValid == false)
            {
                return Result<object>.NotSuccessfull($"Code is invalid for reason: {readTokenResult.Exception}");
            }

            if (readTokenResult.Claims.ContainsKey("uid") == false)
            {
                return Result<object>.NotSuccessfull("Code is invalid, because it isn't contains userId");
            }

            var claimsUserId = long.Parse((string)readTokenResult.Claims["uid"]);

            return claimsUserId == user.Id
                ? Result<object>.Successfull(null!)
                : Result<object>.NotSuccessfull("Code was sent for another user!");
        }
    }
}
