using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IdentityLearning.Application.Options
{
    public class JwtTokenOptions
    {
        public bool ValidateLifeTime { get; set; } = true;
        public bool ValidateIssuer { get; set; } = false;
        public bool ValidateAudience { get; set; } = false;
        public bool ValidateIssuerSigningKey { get; set; } = false;

        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Secret { get; set; }

        public int RefreshTokenExpiresInMinutes { get; set; } = 44640;
        public int AccessTokenExpiresInMinutes { get; set; } = 420;

        public SymmetricSecurityKey? GetSymmetricSecurityKey()
        {
            if (Secret == null)
            {
                return null;
            }

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
        }

        public static TokenValidationParameters GetDeffaultTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = false
            };
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateAudience = ValidateAudience,
                ValidAudience = Audience,
                ValidateLifetime = ValidateLifeTime,
                ValidateIssuerSigningKey = ValidateIssuerSigningKey,
                ValidateIssuer = ValidateIssuer,
                ValidIssuer = Issuer,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }

    }
}
