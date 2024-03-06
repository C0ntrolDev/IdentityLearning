using IdentityLearning.API.Extensions;
using IdentityLearning.API.Filters.AccessTokenBlacklistAuthorize;
using IdentityLearning.API.ModelBinders;
using IdentityLearning.Application;
using IdentityLearning.Application.Options;
using IdentityLearning.Identity;
using IdentityLearning.Infrastructure;
using IdentityLearning.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace IdentityLearning.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<JwtTokenOptions>(builder.Configuration.GetSection("JwtTokenOptions"));

            builder.Services.Configure<EmailServiceOptions>(builder.Configuration.GetSection("EmailServiceOptions"));
            builder.Services.Configure<EmailServiceOptions>(o =>
            {
                var baseUrl = builder.WebHost.GetSetting(WebHostDefaults.ServerUrlsKey);
                o.EmailConfirmationUrl = baseUrl + o.EmailConfirmationUrl;
                o.DeleteAccountConfirmationUrl = baseUrl + o.DeleteAccountConfirmationUrl;
            });

            builder.Services.RegisterIdentity(builder.Configuration);
            builder.Services.RegisterInfrastructure();
            builder.Services.RegisterApplication();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var jwtTokenOptions = builder.Configuration.GetSection("JwtTokenOptions").Get<JwtTokenOptions>();

                options.TokenValidationParameters = jwtTokenOptions != null
                    ? jwtTokenOptions.GetTokenValidationParameters()
                    : JwtTokenOptions.GetDeffaultTokenValidationParameters();
            });

            builder.Services.AddAuthorization(authorizationOptions =>
            {
                authorizationOptions.AddExtendedAuthorizeRequirement();
            });
            builder.Services.AddExtendedAuthorizeHandler();


            builder.Services.AddControllers(o =>
            {
                o.ModelBinderProviders.Insert(0, new UserIdModelBinderProvider());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.AddTokenBlacklistCleaning();

            app.MapControllers();

            app.Run();

        }
    }
}
